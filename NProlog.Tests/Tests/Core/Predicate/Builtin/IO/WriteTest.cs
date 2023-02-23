/*
 * Copyright 2018 S. Webber
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
using Org.NProlog.Core.Terms;

namespace Org.NProlog.Core.Predicate.Builtin.IO;

[TestClass]
public class WriteTest : TestUtils
{
    private static readonly string TEXT = "hello, world!";

    private readonly TextWriter redirectedOut = new StringWriter();
    private readonly TextWriter originalOut = Console.Out;

    [TestInitialize]
    public void SetUpStreams() => Console.SetOut(redirectedOut);

    [TestMethod]
    public void TestWriteString()
    {
        var w = IO.Write.DoWrite();
        w.KnowledgeBase = (CreateKnowledgeBase());
        w.Evaluate(new Term[] { new Atom(TEXT) });
        Assert.AreEqual(TEXT, redirectedOut.ToString());
    }

    [TestMethod]
    public void TestWritelnString()
    {
        var w = IO.Write.DoWriteLn();
        w.KnowledgeBase = (CreateKnowledgeBase());
        w.Evaluate(new Term[] { new Atom(TEXT) });
        Assert.AreEqual(TEXT + Environment.NewLine, redirectedOut.ToString());
    }

    [TestCleanup]
    public void RestoreStreams() => Console.SetOut(originalOut);
}
