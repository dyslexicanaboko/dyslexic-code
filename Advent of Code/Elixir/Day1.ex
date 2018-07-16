# Day1.ex
#c("F:\\Dev\\Unfuddle\\Advent of Code\\Elixir\\Day1.ex")

defmodule Day1 do
  def hello do
    IO.puts("Hello World")
  end

  def solver do
    file = "F:\\Dev\\Unfuddle\\Advent of Code\\Elixir\\Day1.Input.txt"
    {:ok, data} = File.read(file)

    chars = to_char_list data #|> Enum.each(&processCharacter(&1))

    #For some reason you have to use an anonymous function here
    #inside of that anonymous function you can call defined functions
    #Enum.each(chars, fn(x) -> processCharacter(x, 0) end)
    forEachChar(chars, 0, 1)
  end

  def test do
    forEachChar('(()(()(', 0, 1)
  end

  #Santa starts at level zero (0)
  #A open ( is up one floor
  #A closed ) is down one floor
  def forEachChar([head | tail], floor, position) do
    #IO.puts(head)

    if 40 == head do
      #IO.puts("up")
      floor = floor + 1
    else
      #IO.puts("down")
      floor = floor - 1
    end

    #Positon is specifically for part II
    if floor == -1 do
      IO.puts "Entered basement at position: "
      IO.puts position
    end

    position = position + 1

    forEachChar(tail, floor, position)
  end

  def forEachChar([], floor, position) do
    floor
  end
end
