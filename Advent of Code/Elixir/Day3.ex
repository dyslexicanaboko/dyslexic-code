# Day3.ex
#c("F:\\Dev\\Unfuddle\\Advent of Code\\Elixir\\Day3.ex")
#c("C:\Dev\Unfuddle\Advent of Code\Elixir\Day3.ex")

defmodule Day3 do
  def solver do
    #file = "F:\\Dev\\Unfuddle\\Advent of Code\\Elixir\\Day3.Input.txt"
    file = "C:\\Dev\\Unfuddle\\Advent of Code\\Elixir\\Day3.Input.txt"

    {:ok, data} = File.read(file)

    startingPoint(data)
  end

  def test do
    #x = "^>v<" #4 houses
    x = "^v^v^v^v^v" #2 houses
    startingPoint(x)
  end

  defmodule Point do
    defstruct [x: 0, y: 0]
  end

  defmodule Tracker do
    defstruct [map: Map.new, santa1: %Point{}, santa2: %Point{}]
  end

  defp startingPoint(data) do
    list = to_char_list data
    t = %Tracker{}

    m = t.map
    p = t.santa1

    #Initialize the very first house at (0, 0)
    m = Map.put_new(m, p, 1)

    t.map = m

    coordinatesMap = forEachDirection(list, t)

    IO.puts "Final map"
    IO.inspect coordinatesMap

    IO.puts "Houses visited"
    l = length Map.keys(coordinatesMap)
    {l, coordinatesMap}
  end

  def forEachDirection([head | tail], tracker) do
    m = tracker.map
    x = tracker.santa1.x
    y = tracker.santa1.y

    #IO.puts(head)

    case IO.chardata_to_string([head]) do
      "<" -> x = x - 1
      ">" -> x = x + 1
      "^" -> y = y + 1
      "v" -> y = y - 1
    end

    tracker.santa1.x = x
    tracker.santa1.y = y

    if Map.has_key?(m, tracker.santa1) do
      #IO.puts "before"
      #IO.inspect m
      {_, m} = Map.get_and_update(m, tracker.santa1, fn presents -> {presents, presents + 1} end)
      #IO.puts "after"
      #IO.inspect m
    else
      m = Map.put_new(m, tracker.santa1, 1)
    end

    #IO.inspect m

    forEachDirection(tail, tracker)
  end

  def forEachDirection([], tracker) do
    tracker.map
  end
end
