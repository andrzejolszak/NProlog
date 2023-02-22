/*
 * Copyright 2018 S. Webber
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
namespace Org.NProlog.Core.Kb;

/**
 * Implementation of {@link ProjogProperties} with hard-coded values.
 * <p>
 * This class exists as a convenience for creating custom implementations of {@link ProjogProperties}. Custom
 * implementations can extend this class and just override the method(s) they require to be different, rather than
 * implementing {@link ProjogProperties} directly.
 * </p>
 * if this project is upgraded from Java 7 then this class can be removed and its functionality implemented as
 * default methods of the ProjogProperties interface.
 */
public class PrologDefaultProperties : PrologProperties
{
    /**
     * The file to consult when a new {@link Projog} instance is created.
     * <p>
     * Used to initialise the knowledge base.
     *
     * @see #getBootstrapScript()
     */
    public string DEFAULT_BOOTSTRAP_SCRIPT = "prolog-bootstrap.pl";

    /** Returns {@link #DEFAULT_BOOTSTRAP_SCRIPT} */
    public string DEFAULT_BOOTSTRAP_SCRIPT_FOLDER = "Resources";

    public string BootstrapScript => Path.Combine(
        DEFAULT_BOOTSTRAP_SCRIPT_FOLDER, 
        DEFAULT_BOOTSTRAP_SCRIPT);
}
