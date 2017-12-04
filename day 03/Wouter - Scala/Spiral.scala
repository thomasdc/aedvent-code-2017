object Spiral extends App {

  val toFind = 325489
  val arraySize = 571
  var curValue = arraySize * arraySize
  val leftAndBottomBoundry: Int = arraySize / 2 * -1
  val lrightAndUpBoundry: Int = arraySize / 2
  var curX: Int = arraySize / 2
  var curY: Int = arraySize /2 * -1
  var currentDirection = 'L'

  var grid = List[Point2]()

  while (curValue > toFind) {
    addToList(curX, curY, curValue)
    updateCoords()
    curValue -= 1
  }
  println("[%d,%d] - %d".format(curX, curY, curValue))
  println(manhattan(curX, curY))


  def updateCoords(): Unit = {
    currentDirection match {
      case 'L' if canGoLeft => goLeft()
      case 'L' => currentDirection = 'U'; updateCoords()
      case 'U' if canGoUp => goUp()
      case 'U' => currentDirection = 'R'; updateCoords()
      case 'R' if canGoRight => goRight()
      case 'R' => currentDirection = 'D'; updateCoords()
      case 'D' if canGoDown => goDown()
      case 'D' => currentDirection = 'L'; updateCoords()
      case _ => println("Unexpected behavior")
    }

  }

  def manhattan(x: Int, y: Int) = x.abs + y.abs

  def canGoLeft : Boolean = !grid.contains(new Point2(curX-1, curY, 0)) && curX - 1 != leftAndBottomBoundry -1
  def canGoRight = !grid.contains(new Point2(curX+1, curY, 0)) && curX + 1 != lrightAndUpBoundry +1
  def canGoUp = !grid.contains(new Point2(curX, curY+1, 0)) && curY + 1 != lrightAndUpBoundry +1
  def canGoDown = !grid.contains(new Point2(curX, curY-1, 0)) && curY - 1 != leftAndBottomBoundry -1

  def goLeft(): Unit = curX -= 1
  def goRight(): Unit = curX += 1
  def goUp(): Unit = curY += 1
  def goDown(): Unit =  curY -= 1

  def addToList(x: Int, y: Int, value: Int): Unit = {
    grid = new Point2(x , y ,value) :: grid
  }

  class Point2(val x: Int, val y: Int, val value: Int) {
    override def equals(that: Any): Boolean =
      that match {
        case that: Point2 => that.x == this.x && that.y == this.y
        case _ => false
      }
  }


}

