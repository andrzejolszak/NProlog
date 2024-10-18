namespace CSPrologTest
{
    [TestClass]
    public class TestScenarios
    {
        [TestMethod]
        [DataRow("a :- true.", "a")]
        [DataRow("a :- X=2.", "a")]
        [DataRow("a :- 1=2.", "\\+a")]
        [DataRow("a :- b, c, d. b. c:-true. d:-2=2.", "a")]
        [DataRow("a(X) :- X=2.", "a(2)")]
        [DataRow("a :- X=2, b(X). b(X) :- X=2.", "a")]
        [DataRow("a :- b(X), X=2. b(X) :- X=2.", "a")]
        [DataRow("a :- X=1, b(X). b(X) :- X=2.", "\\+a")]
        [DataRow("a :- b(X), X=1. b(X) :- X=2.", "\\+a")]
        [DataRow("a(X) :- X=2.", "\\+a(1)")]
        [DataRow("a :- \\+ fail.", "a")]
        [DataRow("a :- fail.", "\\+a")]
        [DataRow("a :- X=2, X=3.", "\\+a")]
        [DataRow("a :- b(X), c(X). b(X) :- X=3. c(X) :- X=2.", "\\+a")]
        [DataRow("a :- b(2), b(3). b(X) :- Y=X.", "a")]
        [DataRow("a :- b(2, Y), b(3, Z), Y=Z. b(X, Y) :- Y=X.", "\\+a")]
        [DataRow("a :- b(3, Y), b(3, Z), Y=Z. b(X, Y) :- Y=X.", "a")]
        public void TrivialCases(string test, string query)
        {
            query.True(test);
        }

        [TestMethod]
        //[DataRow(@"a :- ""str"" = ['s', 't', 'r'].", "a")]
        [DataRow(@"a :- [s, t, r] = ['s', 't', 'r'].", "a")]
        //[DataRow(@"a :- ""str"" = [s, t, r].", "a")]
        [DataRow(@"a(X) :- ""str"" = X.", @"a(""str"")")]
        [DataRow("a :- var(X).", "a")]
        [DataRow("a :- nonvar(X).", "\\+a")]
        [DataRow("a :- var(1).", "\\+a")]
        [DataRow("a :- nonvar(1).", "a")]
        [DataRow("a :- X = 2, var(X).", "\\+a")]
        [DataRow("a :- X = Y, var(X).", "a")]
        public void BuiltIns(string test, string query)
        {
            query.True(test);
        }

        [TestMethod]
        [DataRow("T: call(!)")]
        [DataRow("F: call(fail)")]
        [DataRow("F: call((fail, _X))")]
        [DataRow("F: call((fail, call(1)))")]
        [DataRow("R: call(_X)")]
        [DataRow("R: call(1)")]
        [DataRow("R: call((fail, 1))")]
        [DataRow("R: call((1; true))")]
        public void call(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow("T: !")]
        [DataRow("F: (!,fail;true)")]
        [DataRow("T: (call(!),fail;true)")]
        [DataRow("T: call(!),fail;true")]
        public void cut(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow("T: ';'(true, fail)")]
        [DataRow("F: ';'((!, fail), true)")]
        [DataRow("T: ';'(!, call(3))")]
        [DataRow("T: ';'((X=1, !), X=2)")]
        [DataRow("T: X=1; X=2")]
        [DataRow("T: (X=1; X=2), X=2")]
        [DataRow("F: (fail; fail), true")]
        [DataRow("T: X=2, (Y=3 ; Y=2), X=Y")]
        [DataRow("F: X=2, (Y=3 ; Y=1), X=Y")]
        [DataRow("T: true, (true ; false)")]
        [DataRow("T: true, (false ; true)")]
        [DataRow("F: false, (false ; true)")]
        [DataRow("F: false, (true ; false)")]
        [DataRow("T: true, true ; false")]
        [DataRow("T: true, false ; true")]
        [DataRow("T: false, false ; true")]
        [DataRow("F: false, true ; false")]
        public void or(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow("T: true")]
        [DataRow("F: fail")]
        public void trueFail(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow("F: X=1, var(X)")]
        [DataRow("T: var(X), X=1")]
        [DataRow("T: (var(X), X=1), Y=2")]
        [DataRow("T: (var(X)), X=1")]
        [DataRow("F: fail, call(3)")]
        [DataRow("F: nofoo(X), call(X)")]
        [DataRow("T: X = true, call(X)")]
        public void and(string test)
        {
            test.Evaluate();
        }

        [TestMethod]
        [DataRow("T: '=\\='(0,1)")]
        [DataRow("F: '=\\='(1.0,1)")]
        [DataRow("F: '=\\='(3 * 2,7 - 1)")]
        [DataRow("R: '=\\='(N,5)")]
        [DataRow("R: '=\\='(floot(1),5)")]
        public void ari_cmp(string test)
        {
            test.Evaluate();
        }
    }
}