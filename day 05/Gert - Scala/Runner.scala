package main.scala.y2017.Day05

object Runner extends Runner {}

class Runner {
  class Context(val offsets: Array[Int], var position: Int) {
    def update(newOffset: Int) {
      val currentOffset = offsets(position)
      offsets.update(position, newOffset)
      position += currentOffset
    }
  }

  def run(input: List[Int], specialIncrementing: Boolean = false): Int = {
    var context = new Context(input.toArray, 0)

    var steps = 0
    while(context.position < context.offsets.length) {
      context = step(context, specialIncrementing)
      steps = steps + 1
    }

    steps
  }

  def step(context: Context, specialIncrementing: Boolean = false): Context = {
    val currentOffset = context.offsets(context.position)

    val newOffset =
      if(specialIncrementing)
        if(currentOffset >= 3) currentOffset - 1
        else currentOffset + 1
      else
        currentOffset + 1

    context.update(newOffset)
    context
  }
}
