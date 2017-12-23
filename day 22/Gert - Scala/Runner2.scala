package main.scala.y2017.Day22

object Runner2 extends Runner {
  override def states = List(0, 1, 2, 3) // Clean, Weakened, Infected, Flagged

  override def newDirection(dir: (Int, Int), health: Int): (Int, Int) = {
    health match {
      case 0 => turn(dir, 1) // left
      case 1 => dir
      case 2 => turn(dir, -1) // right
      case 3 => turn(dir, 2) // reverse
    }
  }

  override def infected: Int = 2
}
