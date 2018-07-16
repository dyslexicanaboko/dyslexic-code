# Day2.ex
#c("F:\\Dev\\Unfuddle\\Advent of Code\\Elixir\\Day2.ex")
defmodule Day2 do
  def solver do
    file = "F:\\Dev\\Unfuddle\\Advent of Code\\Elixir\\Day2.Input.txt"
    {:ok, data} = File.read(file)

    lstPresents = String.split(data, "\r\n")

    forEachPresent(lstPresents, 0, 0)
  end

  def test do
    #IO.puts getPresentTotalSurfaceArea(2, 3, 4)
    #IO.puts getPresentTotalSurfaceArea(1, 1, 10)
    x = """
    2x3x4
    1x1x10
    """

    forEachPresent(String.split(x, "\n"), 0, 0) #101 is the expected result
  end

  def forEachPresent([head | tail], squareFeet, linearFeetRibbon) do
    #IO.puts(head)

    if head != "" do
      [l, w, h] = String.split(head, "x")
      [il, iw, ih] = [String.to_integer(l), String.to_integer(w), String.to_integer(h)]
      [a, b, c] = Enum.sort([il, iw, ih])

      #Part I
      squareFeet = squareFeet + getSurfaceAreaOfCube(il, iw, ih) + getSlackSurfaceArea(a, b)

      #Part II
      linearFeetRibbon = linearFeetRibbon + getRibbonLength(a, b) + getBowLength(il, iw, ih)

      forEachPresent(tail, squareFeet, linearFeetRibbon)
    else
      forEachPresent([], squareFeet, linearFeetRibbon)
    end
  end

  def forEachPresent([], squareFeet, linearFeetRibbon) do
    #This is for part I
    IO.puts ["Total Square Feet: ", Integer.to_string(squareFeet)]

    #This is for part II
    IO.puts ["Total Linear Feet: ", Integer.to_string(linearFeetRibbon)]

    {squareFeet, linearFeetRibbon}
  end

  def getSurfaceAreaOfCube(l, w, h) do
    2*l*w + 2*w*h + 2*h*l
  end

  def getSlackSurfaceArea(a, b) do
    a*b
  end

  def getRibbonLength(a, b) do
    2*a + 2*b
  end

  def getBowLength(l, w, h) do
    l*w*h
  end
end
