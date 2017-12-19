package main.scala.y2017.Day18

class Runner {
  type Result = (Int, Option[BigInt]) // new pointer, stop condition

  val registers = List(scala.collection.mutable.Map[String, BigInt]())
  val queues = List(scala.collection.mutable.ListBuffer[BigInt]())

  def run(commands: Array[String]): BigInt = {
    var pointer = 0
    while(pointer >= 0 && pointer < commands.length) {
      val result = exec(commands(pointer), pointer, 0)
      pointer = result._1

      if(result._2.isDefined)
        return result._2.get
    }

    -1
  }

  def isDigits(s: String): Boolean = s.toCharArray.exists(c => c.isDigit)

  def getValue(param: String, program: Int): BigInt =
    if(isDigits(param)) BigInt(Integer.parseInt(param))
    else registerValue(param, program)

  def registerValue(register: String, program: Int): BigInt =
    if(registers(program).contains(register)) registers(program).get(register).get
    else BigInt(0)

  def exec(command: String, pointer: Int, program: Int): Result = {
    command.take(3) match {
      case "snd" => snd(command, pointer, program)
      case "set" => set(command, pointer, program)
      case "add" => add(command, pointer, program)
      case "mul" => mul(command, pointer, program)
      case "mod" => mod(command, pointer, program)
      case "rcv" => rcv(command, pointer, program)
      case "jgz" => jgz(command, pointer, program)
      case cmd => throw new Exception("Command %s not supported".format(cmd))
    }
  }

  def snd(command: String, pointer: Int, program: Int): Result = {
    queues((program + 1) % queues.length).append(getValue(command.drop(4), program))
    (pointer + 1, None)
  }

  def set(command: String, pointer: Int, program: Int): Result = {
    val params = command.drop(4).split(" ")
    registers(program).put(params(0), getValue(params(1), program))
    (pointer + 1, None)
  }

  def add(command: String, pointer: Int, program: Int): Result = {
    val params = command.drop(4).split(" ")
    registers(program).put(params(0),
      registerValue(params(0), program) + getValue(params(1), program))
    (pointer + 1, None)
  }

  def mul(command: String, pointer: Int, program: Int): Result = {
    val params = command.drop(4).split(" ")
    registers(program).put(params(0),
      registerValue(params(0), program) * getValue(params(1), program))
    (pointer + 1, None)
  }

  def mod(command: String, pointer: Int, program: Int): Result = {
    val params = command.drop(4).split(" ")
    registers(program).put(params(0),
      registerValue(params(0), program) % getValue(params(1), program))
    (pointer + 1, None)
  }

  def rcv(command: String, pointer: Int, program: Int): Result = {
    if(queues(program).nonEmpty) (pointer + 1, Some(queues(program).last))
    else (pointer, None)
  }

  def jgz(command: String, pointer: Int, program: Int): Result = {
    val params = command.drop(4).split(" ")
    if(getValue(params(0), program) > BigInt(0))
      (pointer + getValue(params(1), program).toInt, None)
    else (pointer + 1, None)
  }
}
