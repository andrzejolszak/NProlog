?- pl_add_predicate(meta_data/2, 'org.prolog.core.predicate.udp.PredicateMetaData').

p1(a) :- write(a).
p1(b) :- write(b).
p1(c) :- write(c).

p2(d) :- write(d).
p2(e) :- write(e).
p2(f) :- write(f).

p3(g) :- write(g).
p3(h) :- write(h).
p3(i) :- write(i).

p4(x) :- p1(b), p2(d), p3(i).
p4(y) :- p1(a), p2(f), p3(h).
p4(z) :- p1(c), p2(e), p3(g).

p5(X) :- p4(X).

p6 :- p5(x).

%?- p5(X)
%OUTPUT bdi
% X=x
%OUTPUT afh
% X=y
%OUTPUT ceg
% X=z

%?- p5(x)
%OUTPUT bdi
%YES

%?- p5(y)
%OUTPUT afh
%YES

%?- p5(z)
%OUTPUT ceg
%YES

%FAIL p5(w)

%?- p6
%OUTPUT bdi
%YES

%?- meta_data(p4(q), X)
% X=factory_class : org.prolog.core.predicate.udp.StaticUserDefinedPredicateFactory
% X=factory_isRetryable : true
% X=factory_isAlwaysCutOnBacktrack : false
% X=actual_class : org.prolog.core.predicate.udp.StaticUserDefinedPredicateFactory$LinkedHashMapPredicateFactory
% X=actual_isRetryable : true
% X=actual_isAlwaysCutOnBacktrack : false
% X=processed_class : org.prolog.core.predicate.udp.NeverSucceedsPredicateFactory
% X=processed_isRetryable : false
% X=processed_isAlwaysCutOnBacktrack : false

%?- meta_data(p4(x), X)
% X=factory_class : org.prolog.core.predicate.udp.StaticUserDefinedPredicateFactory
% X=factory_isRetryable : true
% X=factory_isAlwaysCutOnBacktrack : false
% X=actual_class : org.prolog.core.predicate.udp.StaticUserDefinedPredicateFactory$LinkedHashMapPredicateFactory
% X=actual_isRetryable : true
% X=actual_isAlwaysCutOnBacktrack : false
% X=processed_class : org.prolog.core.predicate.udp.SingleNonRetryableRulePredicateFactory
% X=processed_isRetryable : false
% X=processed_isAlwaysCutOnBacktrack : false

%?- meta_data(p5(q), X)
% X=factory_class : org.prolog.core.predicate.udp.StaticUserDefinedPredicateFactory
% X=factory_isRetryable : true
% X=factory_isAlwaysCutOnBacktrack : false
% X=actual_class : org.prolog.core.predicate.udp.SingleRetryableRulePredicateFactory
% X=actual_isRetryable : true
% X=actual_isAlwaysCutOnBacktrack : false
% X=processed_class : org.prolog.core.predicate.udp.SingleRetryableRulePredicateFactory
% X=processed_isRetryable : true
% X=processed_isAlwaysCutOnBacktrack : false

%?- meta_data(p5(x), X)
% X=factory_class : org.prolog.core.predicate.udp.StaticUserDefinedPredicateFactory
% X=factory_isRetryable : true
% X=factory_isAlwaysCutOnBacktrack : false
% X=actual_class : org.prolog.core.predicate.udp.SingleRetryableRulePredicateFactory
% X=actual_isRetryable : true
% X=actual_isAlwaysCutOnBacktrack : false
% X=processed_class : org.prolog.core.predicate.udp.SingleRetryableRulePredicateFactory
% X=processed_isRetryable : true
% X=processed_isAlwaysCutOnBacktrack : false

%?- meta_data(p6, X)
% X=factory_class : org.prolog.core.predicate.udp.StaticUserDefinedPredicateFactory
% X=factory_isRetryable : true
% X=factory_isAlwaysCutOnBacktrack : false
% X=actual_class : org.prolog.core.predicate.udp.SingleRetryableRulePredicateFactory
% X=actual_isRetryable : true
% X=actual_isAlwaysCutOnBacktrack : false
% X=processed_class : org.prolog.core.predicate.udp.SingleRetryableRulePredicateFactory
% X=processed_isRetryable : true
% X=processed_isAlwaysCutOnBacktrack : false

