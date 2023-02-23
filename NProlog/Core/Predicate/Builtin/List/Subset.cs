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

namespace Org.NProlog.Core.Predicate.Builtin.List;

/* TEST
%TRUE subset([],[])
%TRUE subset([],[a,b,c])
%TRUE subset([a],[a,b,c])
%TRUE subset([b,c],[a,b,c])
%TRUE subset([a,b,c],[a,b,c])
%TRUE subset([c,a,b],[a,b,c])
%TRUE subset([c,a,c,b,b,c],[b,a,b,a,c])

%FAIL subset([a,b,c,d],[a,b,c])
%FAIL subset([a,b,c],[])

%?- subset([a,b,c,d],[x,y,z|X])
% X=[a,b,c,d|_]
%?- subset([a,b,c,d,e,f],[a,b,e,g|X])
% X=[c,d,f|_]

%?- subset([X,Y,Z],[q,w,e,r,t,y])
% X=q
% Y=q
% Z=q

%?- subset([a,b,Y,Z,d,e,f],[a,b,e,g|X])
% X=[d,f|_]
% Y=a
% Z=a
*/
/**
 * <code>subset(X,Y)</code> - checks if a set is a subset.
 * <p>
 * True if each of the elements in the list represented by <code>X</code> can be unified with elements in the list
 * represented by <code>Y</code>.
 * </p>
 */
public class Subset : AbstractSingleResultPredicate
{ // TODO compare to SWI version

    protected override bool Evaluate(Term subsetTerm, Term set)
    {
        foreach (var element in ListUtils.ToList(subsetTerm))
        {
            if (!ListUtils.IsMember(element, set)) return false;
            set = set.Term;
        }
        return true;
    }
}
