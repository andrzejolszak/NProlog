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
using Org.NProlog.Core.Math;

namespace Org.NProlog.Core.Terms;



/**
 * Represents a value of the primitive type {@code double} as a {@link Term}.
 * <p>
 * DecimalFractions are constant; their values cannot be changed after they are created. DecimalFractions have no
 * arguments.
 */
public class DecimalFraction : Numeric
{
    /// <summary>
    /// TODO:
    /// NOTICE: if you put long.MaxValue into double, it will be long.MaxValue+1
    /// NOTICE: and it will be negative when converting back.
    /// </summary>
    private readonly double value;

    /**
     * @param value the value this term represents
     */
    public DecimalFraction(double value) => this.value = value;

    /**
     * Returns a {@code string} representation of the {@code double} this term represents.
     *
     * @return a {@code string} representation of the {@code double} this term represents
     */

    public string Name => ToString();


    public Term[] Args => TermUtils.EMPTY_ARRAY;


    public int NumberOfArguments => 0;

    /**
     * @throws IndexOutOfRangeException as this implementation of {@link Term} has no arguments
     */

    public Term GetArgument(int index) 
        => throw new IndexOutOfRangeException($"{nameof(index)}:{index}");

    /**
     * Returns {@link TermType#FRACTION}.
     *
     * @return {@link TermType#FRACTION}
     */

    public TermType Type => TermType.FRACTION;


    public bool IsImmutable => true;


    public DecimalFraction Copy(Dictionary<Variable, Variable>? _) => this;


    public DecimalFraction Term => this;


    public bool Unify(Term t)
    {
        var tType = t.Type;
        return tType == TermType.FRACTION
            ? value == ((DecimalFraction)t.Term).value
            : (tType.IsVariable) && (t.Unify(this))
            ;
    }


    public void Backtrack()
    {
        // do nothing
    }

    /**
     * @return the {@code double} value of this term cast to an {@code long}
     */

    public long Long => unchecked((long)value);

    /**
     * @return the {@code double} value of this term
     */

    public double Double => value;


    public DecimalFraction Calculate(Term[] _) => this;


    public override bool Equals(object? o) 
        => o == this || (o is DecimalFraction fraction && value.Equals(fraction.value));


    public override int GetHashCode() => value.GetHashCode();

    /**
     * @return a {@code string} representation of the {@code double} this term represents
     */

    public override string ToString() => this.value.PatchDoubleString();

    Term Term.Copy(Dictionary<Variable, Variable>? sharedVariables) 
        => this.Copy(sharedVariables);

    Term Term.Term => this.Term;

    public Term Bound => this;

    Numeric ArithmeticOperator.Calculate(Term[] args)
        => this.Calculate(args);
}
