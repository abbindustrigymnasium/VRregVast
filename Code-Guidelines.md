### 1. Använd _snake_case_ för variabler och _Title_Case_ för funktioner

```csharp
// Lättare att läsa (snake_case)
float y_position = 0.32
float height_of_player = 1.64

// Svårare att läsa (camelCase)
float yPosition = 0.32
float heightOfPlayer = 1.64

// Funktion (Title_Case)
void Print_Hello(){
    Console.WriteLine("Hello")
}
```

### 2. Använd tydliga variabelnamn istället för förkortningar

```csharp
// Tydligt
float current_player_rotation_y = 0.32
float target_player_rotation_y = 1.64

// Otydligt
float c_play_rot_y = 0.32
float t_play_rot_y = 1.64

// Jobbigt att att använda
float current_player_rotation_around_y_axis = 0.32
float target_player_rotation_around_y_axis = 1.64
```

### 3. Kommentera koden för att förklara vad en funktion eller en otydlig del kod gör

```csharp
// Prints every item exept the last in an array
void Print_Array(string[] option_array){

    // Loops throught the length - 1 to skip newline charachter
	for(int i = 0; i < option_array.Length - 1; i++){
		Console.WriteLine(option_array[i])
	}
}

```
