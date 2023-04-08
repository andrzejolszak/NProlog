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
namespace Org.NProlog.Core.Kb;


/**
 * Collection of configuration properties.
 * <p>
 * Each {@link org.prolog.core.kb.KnowledgeBase} has a single {@code PrologProperties} instance.
 *
 * @see KnowledgeBase#getPrologProperties()
 */
public interface PrologProperties
{

    /**
     * Returns the name of the resource loaded by {@link KnowledgeBaseUtils#bootstrap(KnowledgeBase)}.
     *
     * @return the name of the resource loaded by {@link KnowledgeBaseUtils#bootstrap(KnowledgeBase)}
     * @see KnowledgeBaseUtils#bootstrap(KnowledgeBase)
     */
    string BootstrapScript { get; }
}
