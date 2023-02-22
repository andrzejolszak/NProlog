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

namespace Org.NProlog.Core.Predicate.Builtin.Classify;


/* TEST
%TRUE integer(1)
%TRUE integer(-1)
%TRUE integer(0)
%FAIL integer(1.0)
%FAIL integer(-1.0)
%FAIL integer(0.0)
%FAIL float('1')
%FAIL float('1.0')
%FAIL integer(a)
%FAIL integer(p(1,2,3))
%FAIL integer([1,2,3])
%FAIL integer([])
%FAIL integer(X)
%FAIL integer(_)
*/
/**
 * <code>integer(X)</code> - checks that a term is an integer.
 * <p>
 * <code>integer(X)</code> succeeds if <code>X</code> currently stands for an integer.
 * </p>
 */
public class IsInteger : AbstractSingleResultPredicate
{
    protected override bool Evaluate(Term arg) 
        => arg.Type == TermType.INTEGER;
}
