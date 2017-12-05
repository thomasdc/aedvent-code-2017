object Util {
  def readStdIn = io.Source.stdin.getLines

  def readAllLines : Array[String] = {
    var lines = Array[String]()
    var nextLine = scala.io.StdIn.readLine()
    while (nextLine != ""){
      lines = lines :+ nextLine
      nextLine =scala.io.StdIn.readLine()
    }
    lines
  }
}
