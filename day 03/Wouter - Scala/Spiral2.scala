object Spiral2 extends App {
  val toFind = 325489
  val arraySize = 571
  var curX: Int = arraySize / 2
  var curY: Int = arraySize /2

  var grid = List[Point2]()
  addToList(curX, curY, 1)
  curX += 1

  def fillValue(): Unit = {
    val value = getNeighbours(curX, curY).map(point => point.value).sum
    addToList(curX, curY, value)
    if(value > toFind) {
      println(value)
      System.exit(0)
    }
  }

  for(n <- 3 until  arraySize by 2) {
    for(up <- 1 until n - 1) { fillValue() ; curY += 1 }
    for(left <- 1 to n - 1) { fillValue(); curX -= 1 }
    for(down <- 1 to n - 1) { fillValue(); curY -= 1 }
    for(right <- 1 to n) { fillValue(); curX += 1 }
  }

  def getNeighbours(x: Int, y: Int) = {
    grid.filter {
      case up: Point2 if    up.y == y + 1 &&    up.x == x => true
      case rup: Point2 if   rup.y == y + 1 &&   rup.x == x +1 => true
      case r: Point2 if     r.y == y &&         r.x ==  x + 1 => true
      case rdown: Point2 if rdown.y == y - 1 && rdown.x == x + 1 => true
      case down: Point2 if  down.y == y - 1 &&  down.x == x => true
      case ldown: Point2 if ldown.y == y - 1 && ldown.x == x -1 => true
      case l: Point2 if     l.y == y &&         l.x == x -1 => true
      case lup: Point2 if   lup.y == y + 1  &&  lup.x == x - 1 => true
      case _ => false
    }
  }

  def addToList(x: Int, y: Int, value: Int): Unit = grid = Point2(x , y ,value) :: grid

  case class Point2(x: Int, y: Int, value: Int) {
    override def equals(that: Any): Boolean =
      that match {
        case that: Point2 => that.x == this.x && that.y == this.y
        case _ => false
      }
  }
}