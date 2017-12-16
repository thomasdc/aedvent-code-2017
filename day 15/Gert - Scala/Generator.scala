package main.scala.y2017.Day15

class Generator(seed: Int, multiplier: BigInt, criteria: Int) {
  var lastValue: BigInt = seed
  val divisor: BigInt = 2147483647

  // TODO maybe use a Stream
  def generate(): BigInt = {
    do { lastValue = (lastValue * multiplier) % divisor }
    while(lastValue % criteria != 0)
    lastValue
  }
}