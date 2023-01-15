% see 1.07 of P-99: Ninety-Nine Prolog Problems

f(X,[X]) :- \+ is_list(X).
f([],[]).
f([X|Xs],Zs) :- f(X,Y), f(Xs,Ys), Append(Y,Ys,Zs).

%?- f([a, [b, [c, d], e]], X)
% X=[a,b,c,d,e]
%NO

%?- f([a, [b, [c, d], [e, [f, [g, h, i, j, [k, l, m, [[[n, o, p, q, r], s, t], u], v, w, x, [y], z]]]]]], X)
% X=[a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z]
%NO
