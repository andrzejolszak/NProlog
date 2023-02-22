/*
 * Copyright 2021 S. Webber
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


/* TEST
%?- limit(3, repeat)
%YES
%YES
%YES

%FAIL limit(-1, true)
%FAIL limit(0, true)
%TRUE limit(1, true)
%TRUE limit(2, true)

%FAIL limit(-1, fail)
%FAIL limit(0, fail)
%FAIL limit(1, fail)
%FAIL limit(2, fail)

%TRUE assert(p(a)), assert(p(b)), assert(p(c))

%FAIL limit(-1, p(X))

%FAIL limit(0, p(X))

%?- limit(1, p(X))
% X=a

%?- limit(2, p(X))
% X=a
% X=b

%?- limit(3, p(X))
% X=a
% X=b
% X=c

%?- limit(4, p(X))
% X=a
% X=b
% X=c

%?- X=a, limit(3, p(X))
% X=a

%?- X=b, limit(3, p(X))
% X=b

%?- X=c, limit(3, p(X))
% X=c

%FAIL X=d, limit(3, p(X))

%FAIL X=fail, limit(3, X)

%?- X=true, limit(3, X)
% X=true

%?- X=repeat, limit(3, X)
% X=repeat
% X=repeat
% X=repeat

%?- limit(a, true)
%ERROR Expected Numeric but got: ATOM with value: a
%?- limit(X, true)
%ERROR Expected Numeric but got: VARIABLE with value: X
%?- limit(3, X)
%ERROR Expected an atom or a predicate but got a VARIABLE with value: X
%?- limit(3, 999)
%ERROR Expected an atom or a predicate but got a INTEGER with value: 999
%FAIL limit(3, unknown_predicate)

p(a,1).
p(a,2).
p(a,3).
p(a,4).
p(a,5).
p(b,1).
p(b,2).
p(b,3).
p(c,1).
p(c,3).
p(d(1),1).
p(d(2),2).
p(d(3),2).
p(d(4),2).
p(d(5),5).

%?- limit(3, p(a,X))
% X=1
% X=2
% X=3

%?- limit(6, p(X,2))
% X=a
% X=b
% X=d(2)
% X=d(3)
% X=d(4)

%?- limit(7, p(d(X),X))
% X=1
% X=2
% X=5
*/
/**
 * <code>limit(N, X)</code> - calls the goal represented by a term a maximum number of times.
 * <p>
 * Evaluates the goal represented by <code>X</code> for a maximum of <code>N</code> attempts.
 */
public class Limit : AbstractPredicateFactory, PreprocessablePredicateFactory
{

    protected override Predicate GetPredicate(Term maxAttempts, Term goal)
    {
        var pf = Predicates.GetPredicateFactory(goal);
        return GetLimitPredicate(pf, maxAttempts, goal);
    }

    private static Predicate GetLimitPredicate(PredicateFactory pf, Term maxAttempts, Term goal)
    {
        var p = pf.GetPredicate(goal.Args);
        var n = TermUtils.CastToNumeric(maxAttempts).Long;
        return new LimitPredicate(p, n);
    }

    public virtual PredicateFactory Preprocess(Term term)
    {
        var goal = term.GetArgument(1);
        return PartialApplicationUtils.IsAtomOrStructure(goal) ? new OptimisedLimit(Predicates.GetPreprocessedPredicateFactory(goal)) : this;
    }

    public class OptimisedLimit : PredicateFactory
    {
        private readonly PredicateFactory factory;

        public OptimisedLimit(PredicateFactory pf) => this.factory = Objects.RequireNonNull(pf);

        public virtual Predicate GetPredicate(Term[] args) => GetLimitPredicate(factory, args[0], args[1]);

        public bool IsRetryable => true;
    }

    public class LimitPredicate : Predicate
    {
        private readonly Predicate predicate;
        private readonly long limit;
        private long ctr;

        public LimitPredicate(Predicate predicate, long limit)
        {
            this.predicate = predicate;
            this.limit = limit;
        }

        public virtual bool Evaluate()
        {
            if (CouldReevaluationSucceed)
            {
                ctr++;
                return predicate.Evaluate();
            }
            return false;
        }


        public virtual bool CouldReevaluationSucceed 
            => ctr < limit && (ctr == 0 || predicate.CouldReevaluationSucceed);
    }
}
