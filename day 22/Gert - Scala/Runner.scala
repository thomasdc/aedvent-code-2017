package main.scala.y2017.Day22

object Runner extends Runner

trait Runner {
  val directions = List((0, -1), (-1, 0), (0, 1), (1, 0))
  def states = List(0, 1) // Clean, Infected

  // For example
  def run(bursts: Int): Int = solve(bursts, testState())

  // For puzzle
  def run(bursts: Int, input: Array[String]): Int =
    solve(bursts, collection.mutable.Map(parse(input).toSeq: _*))

  def solve(bursts: Int, initialState: scala.collection.mutable.Map[(Int, Int), Int]): Int = {
    val state = initialState
    val context = ((0, 0), directions.head, 0)

    Stream.iterate(context)(context => {
      val (pos, dir, flamed) = context

      val health = state.getOrElse(pos, 0)
      val newDir = newDirection(dir, health)

      val newHealth = (health + 1) % states.length
      if(newHealth == 0) state.remove(pos)
      else state.update(pos, newHealth)

      val newBurstCount = flamed + (if(newHealth == infected) 1 else 0)
      (move(pos, newDir), newDir, newBurstCount)
    }
    )(bursts)._3
  }

  def move(pos: (Int, Int), dir: (Int, Int)): (Int, Int) = (pos._1 + dir._1, pos._2 + dir._2)

  def newDirection(dir: (Int, Int), health: Int): (Int, Int) = {
    health match {
      case 0 => turn(dir, 1) // left
      case 1 => turn(dir, -1) // right
    }
  }

  def infected: Int = 1

  def testState(): scala.collection.mutable.Map[(Int, Int), Int] =
    scala.collection.mutable.Map((-1, 0) -> infected, (1, -1) -> infected)

  def parse(input: Array[String]): Map[(Int, Int), Int] = {
    val size = Math.floor(input.length / 2).toInt
    input.indices.flatMap(rowNumber => {
      input(rowNumber).toCharArray.indices.map(colNumber => {
        val isInfected = input(rowNumber).charAt(colNumber) == '#'
        (colNumber - size, rowNumber - size) -> (if(isInfected) infected else 0)
      }).filterNot(location => location._2 == 0)
    }).toMap
  }

  def turn(dir: (Int, Int), aim: Int): (Int, Int) =
    directions((directions.indexOf(dir) + aim + directions.length) % directions.length)
}
