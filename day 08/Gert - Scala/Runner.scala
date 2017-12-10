package main.scala.y2017.Day08

object Runner {
  type Command = (String, String, Int, String, String, Int)

  def run(input: List[String], part2: Boolean = false): Int = {
    var register = scala.collection.mutable.Map[String, Int]()
    var highestRegisterEvah = 0

    input.map(parse).foreach(command => {
      if (checkCondition(command, register)) {
        register = updateRegister(command, register)
        highestRegisterEvah = Math.max(highestRegisterEvah, register.values.max)
      }
    })

    if(part2) highestRegisterEvah
    else register.values.max
  }

  def updateRegister(command: Command, register: scala.collection.mutable.Map[String, Int]): scala.collection.mutable.Map[String, Int] = {
    val initialValue = register.getOrElse(command._1, 0)
    val newRegisterValue = command._2 match {
      case "inc" => initialValue + command._3
      case "dec" => initialValue - command._3
      case op => throw new Exception("Incrementor %s not supported".format(op))
    }

    if(register.contains(command._1))
      register.put(command._1, newRegisterValue)
    else
      register.update(command._1, newRegisterValue)

    register
  }

  def checkCondition(command: Command, register: scala.collection.mutable.Map[String, Int]): Boolean = {
    val registerValue = register.getOrElse(command._4, 0)
    command._5 match {
      case ">" => registerValue > command._6
      case "<" => registerValue < command._6
      case ">=" => registerValue >= command._6
      case "<=" => registerValue <= command._6
      case "==" => registerValue == command._6
      case "!=" => registerValue != command._6
      case op => throw new Exception("Operator %s not supported".format(op))
    }
  }

  def parse(input: String): Command = {
    val parts = input.split(" ")
    (parts(0), parts(1), parts(2).toInt, parts(4), parts(5), parts(6).toInt)
  }
}
