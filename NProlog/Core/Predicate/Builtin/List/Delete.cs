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
using Org.NProlog.Core.Predicate.Builtin.Db;
using Org.NProlog.Core.Terms;

namespace Org.NProlog.Core.Predicate.Builtin.List;




/* TEST
%?- delete([a,b,c],a,X)
% X=[b,c]
%?- delete([a,a,b,a,b,b,c,b,a],a,X)
% X=[b,b,b,c,b]
%?- delete([a,b,c],b,X)
% X=[a,c]
%?- delete([a,b,c],c,X)
% X=[a,b]
%?- delete([a,b,c],z,X)
% X=[a,b,c]
%?- delete([],a,X)
% X=[]

%?- delete([a,b,X],a,[Y,c])
% X=c
% Y=b

%?- delete([a,b,c],Y,X)
% X=[a,b,c]
% Y=UNINSTANTIATED VARIABLE
%?- delete([a,Y,c],b,X)
% X=[a,Y,c]
% Y=UNINSTANTIATED VARIABLE
%?- delete([a,Y,_],_,X)
% X=[a,Y,_]
% Y=UNINSTANTIATED VARIABLE
%?- W=Y,delete([a,Y,_],W,X)
% X=[a,_]
% W=UNINSTANTIATED VARIABLE
% Y=UNINSTANTIATED VARIABLE

%?- delete([],a,X)
% X=[]

% Note: unlike SWI Prolog, fails if the first argument is a partial list.
%FAIL delete([a,b,c|X],Y,Z)
*/
/**
 * <code>delete(X,Y,Z)</code> - Remove all occurrences of a term from a list.
 * <p>
 * Removes all occurrences of the term <code>Y</code> in the list represented by <code>X</code> and attempts to unify
 * the result with <code>Z</code>. Strict term equality is used to identify occurrences.
 */
public class Delete : AbstractSingleResultPredicate
{

    protected override bool Evaluate(Term input, Term element, Term output)
    {
        var list = ListUtils.ToList(input);
        if (list == null) return false;

        list.RemoveAll(item => TermUtils.TermsEqual(element, item));

        return output.Unify(ListFactory.CreateList(list));
    }
}
