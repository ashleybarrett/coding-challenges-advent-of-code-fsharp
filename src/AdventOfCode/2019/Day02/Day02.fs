namespace AdventOfCode.Solutions

module Day02 = 
    open AdventOfCode.Helpers.File

    let solution =

        let filePath = sprintf "%s/%s" __SOURCE_DIRECTORY__ "Input.txt"

        let input = 
            readSingleLineFileAndSplit filePath ','
            |> List.ofArray
            |> List.map int

        let sub (newValue: int) (position: int) (currentInput: int list) = 
            List.mapi (fun i v -> if i = position then newValue else v) currentInput

        let rec processInput (startingPosition: int) (currentInput: int list) = 
            match currentInput.[startingPosition..] with
            | 1 :: numberOne :: numberTwo :: position :: _ -> 
                let add = currentInput.[numberOne] + currentInput.[numberTwo]
                let next = sub add position currentInput

                processInput (startingPosition + 4) next
            | 2 :: numberOne :: numberTwo :: position :: _ -> 
                let multiple = currentInput.[numberOne] * currentInput.[numberTwo]
                let next = sub multiple position currentInput

                processInput (startingPosition + 4) next
            | _ -> currentInput.Head

        let getFinalZeroPositionValue (addressOne: int) (addressTwo: int) =
            input 
            |> sub addressOne 1
            |> sub addressTwo 2
            |> processInput 0            

        let partOne = getFinalZeroPositionValue 12 2

        let partTwo = 
            let (noun, verb) = 
                seq {
                    for numberOne in [|0..99|] do
                    for numberTwo in [|0..99|] do
                        yield (numberOne, numberTwo)
                } 
                |> Seq.find(fun (addressOne, addressTwo) -> 
                    let positionZero = getFinalZeroPositionValue addressOne addressTwo
                    positionZero = 19690720
                )

            100 * noun + verb

        (partOne, partTwo)

    
    
    
