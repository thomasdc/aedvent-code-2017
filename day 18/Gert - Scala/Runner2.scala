package main.scala.y2017.Day18

class Runner2 extends Runner {
  override val registers = List(
    scala.collection.mutable.Map[String, BigInt](),
    scala.collection.mutable.Map[String, BigInt]())

  override val queues = List(
    scala.collection.mutable.ListBuffer[BigInt](),
    scala.collection.mutable.ListBuffer[BigInt]()
  )

  var program1SendCount = 0

  override def run(commands: Array[String]): BigInt = {
    val pointers = scala.collection.mutable.ArrayBuffer(0, 0)
    val waiting = scala.collection.mutable.ArrayBuffer(false, false)

    while(!pointers.forall(pointer => pointer < 0 || pointer >= commands.length)) {
      (0 to 1).foreach(program => {
        val result = exec(commands(pointers(program)), pointers(program), program)

        if(result._1 == pointers(program)) waiting.update(program, true)
        else waiting.update(program, false)

        if(waiting.forall(waiting => waiting))
          return program1SendCount

        pointers.update(program, result._1)
      })
    }

    program1SendCount
  }

  override def registerValue(register: String, program: Int): BigInt = {
    if(registers(program).contains(register))
      registers(program).get(register).get
    else if(register == "p")
      BigInt(program)
    else
      BigInt(0)
  }

  override def snd(command: String, pointer: Int, program: Int): Result = {
    if(program == 1) program1SendCount += 1
    super.snd(command, pointer, program)
  }

  override def rcv(command: String, pointer: Int, program: Int): Result = {
    if(queues(program).nonEmpty) {
      registers(program).put(command.drop(4), queues(program).head)
      queues(program).remove(0, 1)
      (pointer + 1, None)
    } else
      (pointer, None)
  }
}