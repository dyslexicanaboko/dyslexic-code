# Day4.ex
#c("F:\\Dev\\Unfuddle\\Advent of Code\\Elixir\\Day4.ex")
defmodule Day4 do
  def solver do
    input = "bgvyzdsv"

    #zeros = "00000" #5 zeros for part 1
    zeros = "000000" #6 zeros for part 2

    startingPoint(input, zeros)
  end

  def test do
    x = "pqrstuv" #answer 1048970
    #x = "abcdef" #answer 609043
    zeros = "00000"

    startingPoint(x, zeros)
  end

  defp startingPoint(key, zeros) do
    #This will get you a bit array
    #:crypto.hash(:md5, "abcdef609043")

    #This will get you an MD5 hash that is written out as a string
    #:crypto.hash(:md5, "abcdef609043") |> Base.encode16

    #This will get the first two bits from this binary
    #binary_part(<<0, 0, 1, 219, 191, 163, 165, 200, 58, 45, 80, 100, 41, 199, 176, 14>>, 0, 2)

    mineAdventCoins(key, zeros, 1)
  end

  def mineAdventCoins(key, zeros, i) do
    input = Enum.join([key, Integer.to_string(i)])

    binHash = :crypto.hash(:md5, input)

    md5HashString = binHash |> Base.encode16

    #This can be adjusted to look for leading zeros of whatever number required
    #Part 1 wanted 5 zeros, Part 2 wanted 6 zeros
    if String.starts_with? md5HashString, zeros do
      IO.puts "MD5 Hash input:"
      IO.puts input

      IO.puts "MD5 Hash output:"
      IO.puts md5HashString

      md5HashString
    else
      i = i + 1

      mineAdventCoins(key, zeros, i)
    end
  end
end
