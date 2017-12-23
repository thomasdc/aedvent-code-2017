package main.scala.y2017.Day23

class Runner {
  type Result = (Int, Int) // new pointer, stop condition

  val registers = List(scala.collection.mutable.Map[String, BigInt]())

  def run(commands: Array[String]): BigInt = {
    var pointer = 0
    var resultCounter = 0
    while(pointer >= 0 && pointer < commands.length) {
      val result = exec(commands(pointer), pointer, 0)
      pointer = result._1
      resultCounter += result._2
    }

    resultCounter
  }

  // Calculate how many non-prime numbers there are between 109.900 and 126900 in jumps of 17 (a total of 1.001 numbers)
  def run2(): Int = Stream.from(109900, 17).take(1001).count(isNonPrime)

  def isNonPrime(b: Int): Boolean = (2 to b/2).exists(divisor => b % divisor == 0)

  def isDigits(s: String): Boolean = s.toCharArray.exists(c => c.isDigit)

  def getValue(param: String, program: Int): BigInt =
    if(isDigits(param)) BigInt(Integer.parseInt(param))
    else registerValue(param, program)

  def registerValue(register: String, program: Int): BigInt =
    if(registers(program).contains(register)) registers(program).get(register).get
    else BigInt(0)

  def exec(command: String, pointer: Int, program: Int): Result = {
    command.take(3) match {
      case "set" => set(command, pointer, program)
      case "sub" => sub(command, pointer, program)
      case "mul" => mul(command, pointer, program)
      case "jnz" => jnz(command, pointer, program)
      case cmd => throw new Exception("Command %s not supported".format(cmd))
    }
  }

  def set(command: String, pointer: Int, program: Int): Result = {
    val params = command.drop(4).split(" ")
    registers(program).put(params(0), getValue(params(1), program))
    (pointer + 1, 0)
  }

  def sub(command: String, pointer: Int, program: Int): Result = {
    val params = command.drop(4).split(" ")
    registers(program).put(params(0),
      registerValue(params(0), program) - getValue(params(1), program))
    (pointer + 1, 0)
  }

  def mul(command: String, pointer: Int, program: Int): Result = {
    val params = command.drop(4).split(" ")
    registers(program).put(params(0),
      registerValue(params(0), program) * getValue(params(1), program))
    (pointer + 1, 1)
  }

  def jnz(command: String, pointer: Int, program: Int): Result = {
    val params = command.drop(4).split(" ")
    if(getValue(params(0), program) != BigInt(0))
      (pointer + getValue(params(1), program).toInt, 0)
    else (pointer + 1, 0)
  }
}
