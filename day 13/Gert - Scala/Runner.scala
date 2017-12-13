package main.scala.y2017.Day13

object Runner {
  case class Layer (depth: Int, range: Int)

  def run(input: List[String]): Int = sendPackage(input.map(parse))

  def run2(input: List[String]): Int = delayToSendPackage(input.map(parse))

  def delayToSendPackage(layers: List[Layer]): Int =
    Iterator.from(0).find(delay => !isCaught(layers, delay)).get

  def isCaught(layers: List[Layer], delay: Int = 0): Boolean =
    layers.exists(layer => caught(layer, delay))

  def sendPackage(layers: List[Layer], delay: Int = 0): Int = {
    (0 to layers.map(_.depth).max).map(depth => {
      val layer = layers.find(_.depth == depth)
      if(layer.isDefined) costAtLayer(layer.get, delay) else 0
    }).sum
  }

  def costAtLayer(layer: Layer, delay: Int): Int = {
    val isCaught = caught(layer, delay)
    if(isCaught) layer.depth * layer.range else 0
  }

  def caught (layer: Layer, delay: Int): Boolean =
    (layer.depth + delay) % ((layer.range - 1) * 2) == 0

  def parse(layer: String): Layer = {
    val split = layer.split(": ")
    Layer(split.head.toInt, split(1).toInt)
  }
}
