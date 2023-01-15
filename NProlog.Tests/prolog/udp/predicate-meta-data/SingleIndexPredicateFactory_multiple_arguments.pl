?- pl_add_predicate(meta_data/2, 'org.prolog.core.predicate.udp.PredicateMetaData').

p1(a, 1, x).
p1(b, X, y) :- repeat(2).
p1(c, 3, Z).
p1(a, 4, q).

p2(a, 1, x).
p2(X, 2, y) :- repeat(2).
p2(c, 3, Z).
p2(d, 1, q).

p3(a, 1, x).
p3(b, X, y) :- repeat(2).
p3(X, 3, z).
p3(d, 4, x).

%?- meta_data(p1(_, _, _), X)
% X=factory_class : org.prolog.core.predicate.udp.StaticUserDefinedPredicateFactory
% X=factory_isRetryable : true
% X=factory_isAlwaysCutOnBacktrack : false
% X=actual_class : org.prolog.core.predicate.udp.StaticUserDefinedPredicateFactory$SingleIndexPredicateFactory
% X=actual_isRetryable : true
% X=actual_isAlwaysCutOnBacktrack : false
% X=processed_class : org.prolog.core.predicate.udp.StaticUserDefinedPredicateFactory$SingleIndexPredicateFactory
% X=processed_isRetryable : true
% X=processed_isAlwaysCutOnBacktrack : false

%?- meta_data(p2(_, _, _), X)
% X=factory_class : org.prolog.core.predicate.udp.StaticUserDefinedPredicateFactory
% X=factory_isRetryable : true
% X=factory_isAlwaysCutOnBacktrack : false
% X=actual_class : org.prolog.core.predicate.udp.StaticUserDefinedPredicateFactory$SingleIndexPredicateFactory
% X=actual_isRetryable : true
% X=actual_isAlwaysCutOnBacktrack : false
% X=processed_class : org.prolog.core.predicate.udp.StaticUserDefinedPredicateFactory$SingleIndexPredicateFactory
% X=processed_isRetryable : true
% X=processed_isAlwaysCutOnBacktrack : false

%?- meta_data(p3(_, _, _), X)
% X=factory_class : org.prolog.core.predicate.udp.StaticUserDefinedPredicateFactory
% X=factory_isRetryable : true
% X=factory_isAlwaysCutOnBacktrack : false
% X=actual_class : org.prolog.core.predicate.udp.StaticUserDefinedPredicateFactory$SingleIndexPredicateFactory
% X=actual_isRetryable : true
% X=actual_isAlwaysCutOnBacktrack : false
% X=processed_class : org.prolog.core.predicate.udp.StaticUserDefinedPredicateFactory$SingleIndexPredicateFactory
% X=processed_isRetryable : true
% X=processed_isAlwaysCutOnBacktrack : false

%?- p1(X, Y, Z)
% X=a
% Y=1
% Z=x
% X=b
% Y=UNINSTANTIATED VARIABLE
% Z=y
% X=b
% Y=UNINSTANTIATED VARIABLE
% Z=y
% X=c
% Y=3
% Z=UNINSTANTIATED VARIABLE
% X=a
% Y=4
% Z=q

%?- p2(X, Y, Z)
% X=a
% Y=1
% Z=x
% X=UNINSTANTIATED VARIABLE
% Y=2
% Z=y
% X=UNINSTANTIATED VARIABLE
% Y=2
% Z=y
% X=c
% Y=3
% Z=UNINSTANTIATED VARIABLE
% X=d
% Y=1
% Z=q

%?- p3(X, Y, Z)
% X=a
% Y=1
% Z=x
% X=b
% Y=UNINSTANTIATED VARIABLE
% Z=y
% X=b
% Y=UNINSTANTIATED VARIABLE
% Z=y
% X=UNINSTANTIATED VARIABLE
% Y=3
% Z=z
% X=d
% Y=4
% Z=x

