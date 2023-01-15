/*
 * Copyright 2013-2014 S. Webber
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

namespace Org.NProlog.Core.Math;


/**
 * A {@link Term} that has a numerical value.
 *
 * @see TermUtils#castToNumeric(Term)
 */
public interface Numeric : Term, ArithmeticOperator
{
    /**
     * Returns the value of this numeric as a {@code long}.
     *
     * @return the value of this numeric as a {@code long}
     */
    long Long { get; }

    /**
     * Returns the value of this numeric as a {@code double}.
     *
     * @return the value of this numeric as a {@code double}
     */
    double Double { get; }
}
