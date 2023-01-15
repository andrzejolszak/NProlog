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

namespace Org.NProlog.Core.Predicate.Builtin.Time;



/* TEST
%?- convert_time(0, X)
% X=1970-01-01T00:00:00.000+0000
%TRUE convert_time(0, '1970-01-01T00:00:00.000+0000')

%?- convert_time(1000*60*60*24*500+(1000*60*72), X)
% X=1971-05-16T01:12:00.000+0000
%TRUE convert_time(1000*60*60*24*500+(1000*60*72), '1971-05-16T01:12:00.000+0000')

%?- convert_time(9223372036854775807, X)
% X=292278994-08-17T07:12:55.807+0000
*/
/**
 * <code>convert_time(X,Y)</code> - converts a timestamp to a textual representation.
 */
public class ConvertTime : AbstractSingleResultPredicate
{

    protected override bool Evaluate(Term timestamp, Term text)
    {
        var d = CreateDate(timestamp);
        var a = CreateAtom(d);
        return text.Unify(a);
    }

    private DateTime CreateDate(Term timestamp) => new DateTime(ArithmeticOperators.GetNumeric(timestamp).Long);

    private static Atom CreateAtom(DateTime d)
    {
        // TODO have overloaded versions of convert_time that allow the date format and timezone to be specified?
        //SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss.SSSZ");
        //sdf.setTimeZone(TimeZone.getTimeZone("GMT-0"));

        //return new Atom(sdf.format(d));
        return new Atom(d.ToLongDateString());
    }
}
