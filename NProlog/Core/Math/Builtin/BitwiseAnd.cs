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
namespace Org.NProlog.Core.Math.Builtin;


/* TEST
%?- X is 3 /\ 3
% X=3

%?- X is 3 /\ 7
% X=3

%?- X is 3 /\ 6
% X=2

%?- X is 3 /\ 8
% X=0

%?- X is 43 /\ 27
% X=11

%?- X is 27 /\ 43
% X=11

%?- X is 43 /\ 0
% X=0

%?- X is 0 /\ 0
% X=0
*/
/**
 * <code>/\</code> - performs bitwise addition.
 */
public class BitwiseAnd : AbstractBinaryIntegerArithmeticOperator
{

    protected override long CalculateLong(long n1, long n2) => n1 & n2;
}
