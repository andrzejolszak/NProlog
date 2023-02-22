/*
 * Copyright 2020 S. Webber
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
using Org.NProlog.Core.Terms;

namespace Org.NProlog.Core.Predicate.Builtin.List;



/* TEST
%FAIL min_list([], X)
%FAIL max_list([], X)

%TRUE min_list([1,2,3], 1)
%FAIL min_list([1,2,3], 2)
%FAIL min_list([1,2,3], 3)

%FAIL max_list([1,2,3], 1)
%FAIL max_list([1,2,3], 2)
%TRUE max_list([1,2,3], 3)

%TRUE min_list([7], 7)
%TRUE max_list([7], 7)

%TRUE min_list([2*7], 14)
%FAIL min_list([2*7], 2*7)
%FAIL min_list([14], 2*7)

%TRUE max_list([2*7], 14)
%FAIL min_list([2*7], 2*7)
%FAIL max_list([14], 2*7)

%?- min_list([11*2, 7*3, 92/4], X)
% X=21
%?- max_list([11*2, 7*3, 92/4], X)
% X=23

%?- min_list([Y], X)
%ERROR Cannot get Numeric for term: Y of type: VARIABLE
%?- max_list([[1]], X)
%ERROR Cannot get Numeric for term: .(1, []) of type: LIST

%?- min_list([a], X)
%ERROR Cannot find arithmetic operator: a/0
%?- max_list([q(1,2)], X)
%ERROR Cannot find arithmetic operator: q/2

%?- min_list(Y, X)
%ERROR Expected LIST but got: VARIABLE with value: Y
%?- max_list(a, X)
%ERROR Expected LIST but got: ATOM with value: a

%?- min_list([1,2|Y], X)
%ERROR Expected LIST but got: VARIABLE with value: Y
%?- max_list([1,2|Y], X)
%ERROR Expected LIST but got: VARIABLE with value: Y
*/
/**
 * <code>min_list</code> / <code>max_list</code>
 * <p>
 * <ul>
 * <li><code>min_list(Min, List)</code> - True if Min is the smallest number in List. Fails if List is empty.</li>
 * <li><code>max_list(Max, List)</code> - True if Max is the largest number in List. Fails if List is empty.</li>
 * </ul>
 */
public class ExtremumList : AbstractSingleResultPredicate
{
    private readonly bool findMinimum;

    public static ExtremumList MinList() => new (true);

    public static ExtremumList MaxList() => new (false);

    private ExtremumList(bool findMinimum) => this.findMinimum = findMinimum;


    protected override bool Evaluate(Term input, Term output)
    {
        if (input.Type == TermType.EMPTY_LIST)
            return false;
        TermUtils.AssertType(input, TermType.LIST);

        var operators = ArithmeticOperators;
        var result = operators.GetNumeric(input.GetArgument(0));
        var tail = input.GetArgument(1);
        while (tail.Type != TermType.EMPTY_LIST)
        {
            TermUtils.AssertType(tail, TermType.LIST);

            var next = operators.GetNumeric(tail.GetArgument(0));
            int diff = findMinimum ? NumericTermComparator.Compare(next, result)
                   : NumericTermComparator.Compare(result, next);
            if (diff < 0)
                result = next;

            tail = tail.GetArgument(1);
        }

        return output.Unify(result);
    }
}
