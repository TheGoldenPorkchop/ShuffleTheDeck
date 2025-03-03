'Angel Nava
'Spring 2025
'RCET2265
'ShuffleTheDeck
'Link
Option Strict On
Option Explicit On

Module ShuffleTheDeck

    Sub Main()

        Dim userInput As String
        Dim _lastCard(1) As Integer

        Do
            DisplayBoard()
            Console.WriteLine("I am the shuffler")
            Console.WriteLine("Press d to draw a new card")
            Console.WriteLine("Press c to clear current cards and shuffle again")
            Console.WriteLine("Press u to update mat")
            Console.WriteLine("Press q to quit")
            _lastCard = LastCard()
            userInput = Console.ReadLine()
            Select Case userInput
                Case "d"
                    DrawCard()
                Case "c"
                    CardTracker(0, 0,, True)
                    DrawCard(True)
                Case "u"
                    'update i guess
                Case Else
                    Console.WriteLine(StrDup(80, "*"))
                    Console.WriteLine("ugh")
            End Select

        Loop Until userInput = "q"
        Console.WriteLine("See ya later!")

    End Sub

    Function LastCard(Optional cardNumber As Integer = -1, Optional cardSuite As Integer = -1) As Integer()
        Static _lastCard(1) As Integer

        If cardNumber <> -1 Then
            _lastCard(0) = cardNumber
            _lastCard(1) = cardSuite
        End If

        Return _lastCard
    End Function

    Sub DrawCard(Optional clearCount As Boolean = False)
        Dim temp(,) As Boolean = CardTracker(0, 0) 'creates a local copy of the card tracker array
        Dim cardNumber As Integer
        Dim cardSuite As Integer
        Dim _cardNumber As String
        Dim _cardSuite As String

        Static ballCounter As Integer

        'loop until the current random caed has not already been marked as drawn
        Do
            cardNumber = RandomNumberBetween(0, 12)
            cardSuite = RandomNumberBetween(0, 3)
        Loop Until temp(cardNumber, cardSuite) = False Or ballCounter >= 52

        If clearCount Then
            ballCounter = 0
        Else
            'mark current card as being drawn. Updates the Display
            CardTracker(cardNumber, cardSuite, True)
            ballCounter += 1

            'for displaying
            LastCard(cardNumber, cardSuite)

            Select Case cardNumber
                Case 0
                    _cardNumber = "2"
                Case 1
                    _cardNumber = "3"
                Case 2
                    _cardNumber = "4"
                Case 3
                    _cardNumber = "5"
                Case 4
                    _cardNumber = "6"
                Case 5
                    _cardNumber = "7"
                Case 6
                    _cardNumber = "8"
                Case 7
                    _cardNumber = "9"
                Case 8
                    _cardNumber = "10"
                Case 9
                    _cardNumber = "Jack"
                Case 10
                    _cardNumber = "Queen"
                Case 11
                    _cardNumber = "King"
                Case 12
                    _cardNumber = "Ace"
            End Select

            Select Case cardSuite
                Case 0
                    _cardSuite = "Spades"
                Case 1
                    _cardSuite = "Hearts"
                Case 2
                    _cardSuite = "Clubs"
                Case 3
                    _cardSuite = "Diamonds"
            End Select

            Console.WriteLine(StrDup(80, "*"))
            Console.WriteLine($"the current card is the {_cardNumber} of {_cardSuite} ")
        End If

    End Sub

    Sub DisplayBoard()
        Console.OutputEncoding = System.Text.Encoding.UTF8
        Dim displayString As String = "  |"
        Dim heading() As String = {ChrW(&H2660), ChrW(&H2665), ChrW(&H2666), ChrW(&H2663)}
        Dim tracker(,) As Boolean = CardTracker(0, 0)
        Dim columnWidth As Integer = 5

        For Each letter In heading
            'Console.Write(letter.PadLeft((columnWidth \ 2) + 1).PadRight(columnWidth))
            Console.Write(letter.PadLeft(CInt(Math.Ceiling((columnWidth \ 2) + 1))).PadRight(columnWidth))
        Next
        Console.WriteLine(vbNewLine & StrDup(4 * columnWidth, "_"))



        For cardNumber = 0 To 12 'fix, loop through array
            For cardSuite = 0 To 3 'fix
                If tracker(cardNumber, cardSuite) Then
                    displayString = $"{FormatCardNumber(cardNumber, cardSuite)} |"
                Else
                    displayString = "|"
                End If

                displayString = displayString.PadLeft(columnWidth)
                Console.Write(displayString)

            Next
            Console.WriteLine(vbNewLine & StrDup(4 * columnWidth, "_"))
        Next

    End Sub

    Function CardTracker(cardNumber As Integer,
                      cardSuite As Integer,
                      Optional update As Boolean = False,
                      Optional clear As Boolean = False) As Boolean(,)

        Static _cardTracker(12, 3) As Boolean

        If update Then
            _cardTracker(cardNumber, cardSuite) = True
        End If

        If clear Then
            ReDim _cardTracker(12, 3) 'clears the array. Could also loop through array and set all elements to false
        End If

        Return _cardTracker

    End Function

    Function FormatCardNumber(cardNumber As Integer, cardSuite As Integer) As String
        Dim _cardNumber As String
        _cardNumber = CStr((cardNumber))

        Select Case cardNumber
            Case 0
                _cardNumber = "2"
            Case 1
                _cardNumber = "3"
            Case 2
                _cardNumber = "4"
            Case 3
                _cardNumber = "5"
            Case 4
                _cardNumber = "6"
            Case 5
                _cardNumber = "7"
            Case 6
                _cardNumber = "8"
            Case 7
                _cardNumber = "9"
            Case 8
                _cardNumber = "10"
            Case 9
                _cardNumber = "J"
            Case 10
                _cardNumber = "Q"
            Case 11
                _cardNumber = "K"
            Case 12
                _cardNumber = "A"
        End Select

        '_cardNumber = CStr((cardNumber))
        Return _cardNumber
    End Function
    Function FormatCardSuite(cardSuite As Integer) As String
        Dim _cardSuite As String = "♠"
        Select Case cardSuite
            Case 0
                _cardSuite = "♠"
            Case 1
                _cardSuite = "♥"
            Case 2
                _cardSuite = "♣"
            Case 3
                _cardSuite = "♦"
        End Select

        Return _cardSuite
    End Function

    Function RandomNumberBetween(min As Integer, max As Integer) As Integer
        Dim woag As Single
        max += 1 'ensures max is included for math dot floor
        Randomize()
        woag = Rnd()
        woag *= max - min
        woag += min
        Return CInt(Math.Floor(woag)) 'ok, but max not included
    End Function

End Module
