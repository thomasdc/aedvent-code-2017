package main.scala.y2017.Day24

import org.apache.commons.lang3.time.StopWatch

object Runner {
  type Ports = (Int, Int)
  type Connector = (Ports, Int)

  def run(input: Array[String]): Int = {
    val connectors = input.map(parse).zipWithIndex
    build(0, connectors).map(bridge => (bridge, points(bridge))).maxBy(_._2)._2
  }

  def run2(input: Array[String]): Int = {
    val connectors = input.map(parse).zipWithIndex
    val allBridges = build(0, connectors)
    val maxLength = allBridges.map(_.length).max
    allBridges.filter(_.length == maxLength).map(bridge => (bridge, points(bridge))).maxBy(_._2)._2
  }

  def points(bridge: List[Connector]): Int =
    bridge.map(connector => connector._1._1 + connector._1._2).sum

  def build(port: Int, availableConnectors: Array[Connector]): Seq[List[Connector]] = {
    getFittingConnectors(availableConnectors, port)
      .flatMap(fittingConnector => {
        val portToMatchOn = if(fittingConnector._1._1 == port) fittingConnector._1._2 else fittingConnector._1._1
        val remainingConnectors = availableConnectors.filter(_._2 != fittingConnector._2)

        val possibleTails = build(portToMatchOn, remainingConnectors)

        if (possibleTails.isEmpty)
          Seq(List(fittingConnector))
        else
          possibleTails.map(possibleTail => List(fittingConnector) ::: possibleTail)
      })
  }

  def getFittingConnectors(connectors: Array[Connector], port: Int): Array[Connector] =
    connectors.filter(connector => connector._1._1 == port || connector._1._2 == port)

  def parse(input: String): Ports = {
    val ports = input.split("/").map(_.toInt)
    (ports.head, ports(1))
  }
}
