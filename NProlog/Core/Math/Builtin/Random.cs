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
using Org.NProlog.Core.Terms;

namespace Org.NProlog.Core.Math.Builtin;


/* TEST
validate_in_range(X) :- Y is random(X), Y>=0, Y<X.

%TRUE validate_in_range(3), validate_in_range(7), validate_in_range(100)

%?- X is random(1)
% X=0

test_max_random(X) :- X is random(9223372036854775807), X>=0, X<9223372036854775807.

%?- test_max_random(X), test_max_random(Y), integer(X), integer(Y), X=\=Y, write(here), fail
%OUTPUT here
%NO
*/
/**
 * <code>random(X)</code> Evaluate to a random integer i for which 0 =&lt; i &lt; X.
 */
public class Random : AbstractArithmeticOperator
{

    public override Numeric Calculate(Numeric n) 
        => IntegerNumberCache.ValueOf((long)(System.Random.Shared.NextDouble() * n.Long));

    /** Random is not pure. Multiple calls with the same argument do not produce identical results. */

    public override bool IsPure => false;
}
