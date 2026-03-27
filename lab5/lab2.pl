:- initialization(main, main).

min_odd([H|T], Min) :-
    H mod 2 =:= 1,
    min_odd_helper(T, H, Min).

min_odd([_|T], Min) :-
    min_odd(T, Min).

min_odd([], _) :-
    write('No odd numbers in list'), nl, fail.

min_odd_helper([], Min, Min).

min_odd_helper([H|T], CurrentMin, Min) :-
    ( H mod 2 =:= 1,
      H < CurrentMin
    -> NewMin = H
    ;  NewMin = CurrentMin
    ),
    min_odd_helper(T, NewMin, Min).

main :-
    write('Enter list (example: [1,2,3,4,5]): '), nl,
    read_line_to_string(user_input, Input),
    term_string(List, Input),
    ( min_odd(List, Min)
    -> format('Min odd = ~w~n', [Min])
    ; true
    ),
    halt.