/*
 * Copyright 2013 S. Webber
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a Copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using Org.NProlog.Core.Predicate;
using Org.NProlog.Core.Terms;

namespace Org.NProlog.Examples;


[TestClass]
public class SingletonPredicateExample : AbstractSingleResultPredicate
{
    [TestMethod]
    public void Test() => Assert.IsTrue(true);
    protected override bool Evaluate(Term t1, Term t2)
    {
        var t1ToUpperCase = new Atom(TermUtils.GetAtomName(t1).ToUpper());
        return t2.Unify(t1ToUpperCase);
    }
}
