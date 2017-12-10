package main.scala.y2017.Day01

object Runner {
  def run(input: String): Int = genericRun(input, 1)

  def run2(input: String): Int = genericRun(input, input.length / 2)

  def genericRun(input: String, stepsForward: Int) : Int = {
    (0 until input.length).map(i => {
      val nextPos = (i + stepsForward) % input.length
      if(input.charAt(i) == input.charAt(nextPos)) input.charAt(i).asDigit else 0
    }).sum
  }
}
