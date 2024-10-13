using Org.NProlog.Api;
using Org.NProlog.Core.Predicate;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CSPrologTest
{
    public static class TestUtils
    {
    }

    public static class PrologSourceStringExtensions
    {
        private static readonly string Dynamics = @"
%:- fail_if_undefined( nofoo/1 ).
%:- fail_if_undefined( undef_pred/0 ).
%:- fail_if_undefined( '\\='/2 ).
%:- fail_if_undefined( foo/2 ).
%:- fail_if_undefined( '^'/2 ). 

%:- dynamic( nofoo/1 ).
%:- dynamic( undef_pred/0 ).
%:- dynamic( '\\='/2 ).
%:- dynamic( foo/2 ).
";

        public static PredicateKey CanParse(this string consult, [CallerLineNumber] int sourceLineNumber = 0)
        {
            consult = consult.Replace("\r\n", Environment.NewLine);
            Prolog e = new Prolog();
            e.ConsultString(consult + Dynamics);
            return e.KnowledgeBase.Predicates.GetUserDefinedPredicates().FirstOrDefault().Key;
        }

        public static void True(this string query, string consult = null, bool executionDetails = true, int? solutionsCount = null, [CallerLineNumber] int sourceLineNumber = 0)
        {
            Prolog e = new Prolog();
            e.ConsultString((consult ?? "") + Dynamics);

            if (!query.TrimEnd().EndsWith("."))
            {
                query = query + ".";
            }

            QueryResult ss = e.ExecuteQuery(query);

            Assert.IsTrue(ss.Next(), $"{query} NOT TRUE @ ln {sourceLineNumber}{Environment.NewLine}OUT: {ss}, {Environment.NewLine}{Environment.NewLine}");

            if (solutionsCount.HasValue)
            {
                ss.Exhaust();
                Assert.AreEqual(solutionsCount.Value, ss.SolutionsCount);
            }

            if (consult == null)
            {
                e = new Prolog();
                e.ConsultString("test :- " + query + Dynamics);
                ss = e.ExecuteQuery("test.");

                Assert.IsTrue(ss.Next(), $"test NOT TRUE @ ln {sourceLineNumber}{Environment.NewLine}OUT: {ss}{Environment.NewLine}{Environment.NewLine}");
            }
        }

        public static void False(this string query, string consult = null, bool executionDetails = true, [CallerLineNumber] int sourceLineNumber = 0)
        {
            Prolog e = new Prolog();

            e.ConsultString((consult ?? "")+ Dynamics);

            if (!query.TrimEnd().EndsWith("."))
            {
                query = query + ".";
            }

            QueryResult ss = e.ExecuteQuery(query);

            Assert.IsFalse(ss.Next(), $"{query} NOT FALSE @ ln {sourceLineNumber}{Environment.NewLine}OUT: {ss}{Environment.NewLine}{Environment.NewLine}");

            if (consult == null)
            {
                e = new Prolog();
                e.ConsultString("test :- " + query + Dynamics);
                ss = e.ExecuteQuery("test.");

                Assert.IsFalse(ss.Next(), $"{query} NOT FALSE @ ln {sourceLineNumber}{Environment.NewLine}OUT: {ss}{Environment.NewLine}{Environment.NewLine}");
            }
        }

        public static void Error(this string query, string consult = null, bool executionDetails = true, [CallerLineNumber] int sourceLineNumber = 0)
        {
            Prolog e = new Prolog();

            e.ConsultString((consult ?? "") + Dynamics);

            if (!query.TrimEnd().EndsWith("."))
            {
                query = query + ".";
            }

            try
            {
                QueryResult ss = e.ExecuteQuery(query);
                Assert.Fail($"{query} NOT ERROR @ ln {sourceLineNumber}{Environment.NewLine}OUT: {ss}{Environment.NewLine}{Environment.NewLine}");
            }
            catch (Exception ex)
            {

            }
        }

        public static void Evaluate(this string test, string consult = null, bool executionDetails = true)
        {
            string expectation = test.Substring(0, 3);
            string query = test.Substring(3);

            switch (expectation)
            {
                case "T: ":
                    // TODO: treat as T1 in the future
                    query.True(consult, executionDetails: executionDetails);
                    break;

                case "T1:":
                    query.True(consult, executionDetails: executionDetails, solutionsCount: 1);
                    break;

                case "T2:":
                    query.True(consult, executionDetails: executionDetails, solutionsCount: 2);
                    break;

                case "T3:":
                    query.True(consult, executionDetails: executionDetails, solutionsCount: 3);
                    break;

                case "T4:":
                    query.True(consult, executionDetails: executionDetails, solutionsCount: 4);
                    break;

                case "T5:":
                    query.True(consult, executionDetails: executionDetails, solutionsCount: 5);
                    break;

                case "F: ":
                    query.False(consult, executionDetails: executionDetails);
                    break;

                case "P: ":
                    query.Error(consult, executionDetails: executionDetails);
                    break;

                case "R: ":
                    query.Error(consult, executionDetails: executionDetails);
                    break;

                default:
                    throw new InvalidOperationException("Not supported expectation: " + expectation);
            }
        }
    }
}