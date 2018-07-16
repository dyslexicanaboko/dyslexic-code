#c("F:\\Dev\\Unfuddle\\Advent of Code\\Elixir\\Testing.exs")
exFb = fn
  {0, 0, _} -> IO.puts("FizzBuzz")
  {0, _, _} -> IO.puts("Fizz")
  {_, 0, _} -> IO.puts("Buzz")
end

exFb.({0, 0, 1})
exFb.({0, 1, 2})
exFb.({1, 0, 2})
