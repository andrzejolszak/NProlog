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

% TODO should be able to resolve p5(x) to singleton but does not
% would need to Add optimise method to clause action
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
