/*
 * Copyright 2013 S. Webber
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using Org.NProlog.Core.Exceptions;
using Org.NProlog.Core.Kb;
using Org.NProlog.Core.Terms;
using static System.Net.Mime.MediaTypeNames;

namespace Org.NProlog.Core.Predicate.Udp;

/**
 * Provides support for Definite Clause Grammars (DCG).
 * <p>
 * DCGs provide a convenient way to express grammar rules.
 */
public class DefiniteClauseGrammerConvertor
{
    public static bool IsDCG(Term dcgTerm) 
        => dcgTerm.Type == TermType.STRUCTURE && dcgTerm.NumberOfArguments == 2 && dcgTerm.Name.Equals("-->");

    /**
     * @param dcgTerm predicate with name "-=>" and two arguments
     */
    public static Term Convert(Term dcgTerm, string prefix = "A", List<Variable> vars = null)
    {
        vars ??= new List<Variable>();

        if (IsDCG(dcgTerm) == false)
        {
            throw new PrologException("Expected two argument predicate named \"-->\" but got: " + dcgTerm);
        }

        var consequent = GetConsequent(dcgTerm);
        var antecedent = GetAntecedent(dcgTerm);
        // slightly inefficient as will have already converted to an array in validate method
        var antecedents = KnowledgeBaseUtils.ToArrayOfConjunctions(antecedent);

        return HasSingleListWithSingleAtomElement(antecedents)
            ? convertSingleListTermAntecedent(consequent, antecedents[0], prefix, vars)
            : ConvertConjunctionOfAtomsAntecedent(consequent, antecedents, prefix, vars);
    }

    private static Term convertSingleListTermAntecedent(Term consequent, Term antecedent, string prefix, List<Variable> vars)
    {
        var consequentName = consequent.Name;
        var variable = new Variable(prefix + vars.Count);
        vars.Add(variable);

        var list = ListFactory.CreateList(antecedent.GetArgument(0), variable);
        var args = new Term[consequent.NumberOfArguments + 2];
        for (int i = 0; i < consequent.NumberOfArguments; i++)
        {
            args[i] = consequent.GetArgument(i);
        }
        args[^2] = list;
        args[^1] = variable;
        return Structure.CreateStructure(consequentName, args);
    }

    // TODO this method is too long - refactor
    private static Term ConvertConjunctionOfAtomsAntecedent(Term consequent, Term[] conjunctionOfAtoms, string prefix, List<Variable> vars)
    {
        List<Term> newSequence = new();

        var lastArg = new Variable(prefix + vars.Count);
        vars.Add(lastArg);

        Term previous = lastArg;
        Term? previousList = null;
        for (int i = conjunctionOfAtoms.Length - 1; i > -1; i--)
        {
            var term = conjunctionOfAtoms[i];
            if (term.Name.Equals("{"))
            {
                var newAntecedentArg = term.GetArgument(0).GetArgument(0);
                newSequence.Insert(0, newAntecedentArg);
            }
            else if (term.Type == TermType.LIST)
            {
                if (previousList != null)
                    term = AppendToEndOfList(term, previousList);
                previousList = term;
            }
            else
            {
                if (previousList != null)
                {
                    var _next = new Variable(prefix + vars.Count);
                    vars.Add(_next);

                    var _newAntecedentArg = Structure.CreateStructure("=", new Term[] { _next, AppendToEndOfList(previousList, previous) });
                    newSequence.Insert(0, _newAntecedentArg);
                    previousList = null;
                    previous = _next;
                }

                var newAntecedentArg = CreateNewPredicate(term, previous, vars);
                previous = vars[vars.Count - 1];
                newSequence.Insert(0, newAntecedentArg);
            }
        }

        Term? newAntecedent;
        if (newSequence.Count == 0)
            newAntecedent = null;
        else
        {
            newAntecedent = newSequence[(0)];
            for (int i = 1; i < newSequence.Count; i++)
            {
                newAntecedent = Structure.CreateStructure(KnowledgeBaseUtils.CONJUNCTION_PREDICATE_NAME, new Term[] { newAntecedent, newSequence[(i)] });
            }
        }

        if (previousList != null)
            previous = AppendToEndOfList(previousList, previous);

        var newConsequent = CreateNewPredicate(consequent, lastArg, vars, previous);

        return newAntecedent == null
            ? newConsequent
            : Structure.CreateStructure(KnowledgeBaseUtils.IMPLICATION_PREDICATE_NAME, new Term[] { newConsequent, newAntecedent });
    }

    private static Term AppendToEndOfList(Term list, Term newTail)
    {
        List<Term> terms = new();
        while (list.Type == TermType.LIST)
        {
            terms.Add(list.GetArgument(0));
            list = list.GetArgument(1);
        }
        return ListFactory.CreateList(terms.ToArray(), newTail);
    }

    private static Term CreateNewPredicate(Term original, Term nMinusOneVar, List<Variable> vars, Term nextVar = null)
    {
        if (original.Name == ";" && original.Args.Length == 2)
        {
            // return Structure.CreateStructure(original.Name, new Term[] { CreateNewPredicate(original.Args[0], previousVar, nextVar), CreateNewPredicate(original.Args[1], previousVar, nextVar) });
            List<Variable> vars2 = vars;
            Term first = Convert(Structure.CreateStructure("-->", new[] { new Atom("__a"), original.Args[0] }), vars: vars2);
            Term second = Convert(Structure.CreateStructure("-->", new[] { new Atom("__b"), original.Args[1] }), vars: vars2);
            return Structure.CreateStructure(original.Name, new Term[]
            {
                first.Name == ":-" ?  first.Args[1] : first.Args[0],
                second.Name == ":-" ?  second.Args[1] : second.Args[0]
            });
        }

        var args = new Term[original.NumberOfArguments + 2];
        for (int a = 0; a < original.NumberOfArguments; a++)
        {
            args[a] = original.GetArgument(a);
        }

        if (nextVar is null)
        {
            var next = new Variable("A" + vars.Count);
            vars.Add(next);
            args[original.NumberOfArguments] = next;
        }
        else
        {
            args[original.NumberOfArguments] = nextVar;
        }

        args[original.NumberOfArguments + 1] = nMinusOneVar;
        return Structure.CreateStructure(original.Name, args);
    }

    private static Term GetConsequent(Term dcgTerm) => dcgTerm.GetArgument(0);

    private static Term GetAntecedent(Term dcgTerm) => dcgTerm.GetArgument(1);

    private static bool HasSingleListWithSingleAtomElement(Term[] terms) => terms.Length == 1
               && terms[0].Type == TermType.LIST
               && terms[0].GetArgument(0).Type == TermType.ATOM
               && terms[0].GetArgument(1).Type == TermType.EMPTY_LIST
        ;
}
