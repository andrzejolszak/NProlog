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
using Org.NProlog.Core.Kb;
using Org.NProlog.Core.Predicate.Builtin.List;
using Org.NProlog.Core.Terms;

namespace Org.NProlog.Core.Predicate.Builtin.Compound;


// TODO shouldn't need to wrap disjunctions in brackets. e.g. should be able to do: once(true;true;fail)
/* TEST
%TRUE once(repeat)
%TRUE once(true)
%TRUE once((true,true,true))
%TRUE once((true;true;true))
%TRUE once((fail;true;true))
%TRUE once((true;fail;true))
%TRUE once((true;true;fail))
%FAIL once(fail)
%FAIL once((fail;fail;fail))
%FAIL once((fail,fail,fail))
%FAIL once((true,true,fail))
%FAIL once((true,fail,true))
%FAIL once((fail,true,true))
*/
/**
 * <code>once(X)</code> - calls the goal represented by a term.
 * <p>
 * The <code>once(X)</code> goal succeeds if an attempt to satisfy the goal represented by the term <code>X</code>
 * succeeds. No attempt is made to retry the goal during backtracking - it is only evaluated once.
 * </p>
 */
public class Once : AbstractSingleResultPredicate, PreprocessablePredicateFactory
{
    protected override bool Evaluate(Term t) 
        => Predicates.GetPredicate(t).Evaluate();

    public virtual PredicateFactory Preprocess(Term term)
    {
        var arg = term.GetArgument(0);
        return PartialApplicationUtils.IsAtomOrStructure(arg) 
            ? new OptimisedOnce(Predicates.GetPreprocessedPredicateFactory(arg)) 
            : this;
    }

    public class OptimisedOnce : AbstractSingleResultPredicate
    {
        private readonly PredicateFactory factory;

        public OptimisedOnce(PredicateFactory factory) 
            => this.factory = Objects.RequireNonNull(factory);

        protected override bool Evaluate(Term arg) 
            => factory.GetPredicate(arg.Args).Evaluate();
    }
}
