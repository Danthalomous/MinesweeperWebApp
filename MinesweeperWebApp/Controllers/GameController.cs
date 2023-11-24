document.addEventListener("DOMContentLoaded", function () {
    console.log("gameScript.js Loaded");
    let bombCount = 0; // Keep track of the number of bombs revealed

    // Attach click event listeners for left-clicks
    document.querySelectorAll('.game-button').forEach(button => {
        button.addEventListener('click', function (event) {
            let row = event.target.getAttribute('data-row');
            let col = event.target.getAttribute('data-col');
            console.log("The button clicked's [r,c]: " + row + "," + col); // Log the row and column
            handleCellClick(row, col);
        });
    });

    // Attach contextmenu event listeners for right-clicks to place flags
    document.querySelectorAll('.game-button').forEach(button => {
        button.addEventListener('contextmenu', function (event) {
            event.preventDefault(); // Prevent the context menu from appearing
            let row = event.target.getAttribute('data-row');
            let col = event.target.getAttribute('data-col');
            toggleFlag(row, col);
        });
    });

    document.querySelectorAll('.save-button').forEach(button => {
        button.addEventListener('click', function (event) {
            saveGameState(0, event.target.getAttribute('data-date'), event.target.getAttribute('data-model'));
        });
    });

    // Event listener for the load button
    document.querySelector('.load-button').addEventListener('click', function () {
        loadSavedGames();
    });

    function handleCellClick(row, col) {

        let cellSelector = `[data-row="${row}"][data-col="${col}"]`;
        let cell = document.querySelector(cellSelector);

        if (cell.classList.contains('bomb')) {
            alert('Game Over! You clicked a bomb');
            // Disable further clicks on the game board
            document.querySelectorAll('.game-button').forEach(button => {
                button.removeEventListener('click', handleCellClick);
                button.removeEventListener('contextmenu', toggleFlag);
            });
        }

        // If the cell is already revealed or flagged, don't allow it to be clicked again
        if (cell.classList.contains('revealed') || cell.classList.contains('flag')) {
            return;
        }

        $.ajax({
            method: "POST",
            url: "/Game/CellClicked",
            data: { row: row, col: col },
            success: function (response) {
                updateCell(response);
                if (response.isMine && response.lives <= 0) {
                    alert('Game Over!');
                }
            },
            error: function () {
                alert('An error occurred while processing the click.');
            }
        });
    }

    function toggleFlag(row, col) {
        let cellSelector = `[data-row="${row}"][data-col="${col}"]`;
        let cell = document.querySelector(cellSelector);

        // Check if the cell already has a flag
        if (cell.classList.contains('flag')) {
            // If it does, remove the flag and update the class
            cell.classList.remove('flag');
            cell.innerHTML = ''; // Remove the flag image
            bombCount--;
        } else {
            // If it doesn't, proceed with the AJAX call to add the flag
            bombCount++;
            $.ajax({
                method: "POST",
                url: "/Game/ToggleFlag",
                data: { row: row, col: col },
                success: function (response) {
                    updateCell(response);
                },
                error: function () {
                    alert('An error occurred while toggling the flag.');
                }
            });
        }
    }

    function updateCell(cellData) {
        let cellSelector = `[data-row="${cellData.row}"][data-col="${cellData.col}"]`;
        let cell = document.querySelector(cellSelector);

        checkGameOver();

        if (cell) {
            if (cellData.isRevealed) {
                if (cellData.isMine) {
                    bombCount++; // Increment bomb count when a mine is revealed
                    cell.classList.add('bomb');
                    cell.innerHTML = '<img src="/img/bomb.png" alt="Bomb" />'; // Make sure this path is correct
                    checkGameOver(); // Check if the game is over
                } else {
                    cell.classList.add('revealed');
                    if (cellData.adjacentMines > 0) {
                        cell.textContent = cellData.adjacentMines;
                    }
                }
            }

            if (cellData.isFlagged) {
                cell.classList.toggle('flag'); // Toggle the flag class on or off
                cell.innerHTML = '<img src="/img/flag.png" alt="Flag" />';
            }
        }
    }

    function checkGameOver() {
        console.log(bombCount);
        if (bombCount >= 10) { // Change this number to however many bombs should end the game
            alert('Game Over! You win!');
            // Disable further clicks on the game board
            document.querySelectorAll('.game-button').forEach(button => {
                button.removeEventListener('click', handleCellClick);
                button.removeEventListener('contextmenu', toggleFlag);
            });
        }
    }

    function saveGameState(userID, dateTime, model) {
        console.log("In SaveGameState()");

        var SaveBoardRequest = {
            UserID: userID,
            SavedDate: dateTime,
            GameBoard: model
        };

        console.log(SaveBoardRequest);

        $.ajax({
            url: "/api/GameStateAPI/saveBoard",
            type: "PUT",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(SaveBoardRequest),
            success: function (result) {
                // Handle success if needed
                console.log(result);
            },
            error: function (error) {
                // Handle error if needed
                console.error("There was an error calling the API Route: " + error);
            }
        });
    }

    function loadSavedGames() {
        console.log("Loading saved games");

        // Example AJAX call to fetch saved games
        $.ajax({
            url: "/api/GameStateAPI/getAllSaves", // Adjust the URL as per your routing and API setup
            type: "GET",
            success: function (savedGames) {
                displaySavedGames(savedGames);
            },
            error: function (error) {
                console.error("Error loading saved games: " + error);
            }
        });
    }

    function displaySavedGames(savedGames) {
        // Find or create a container for displaying saved games
        let savedGamesContainer = document.getElementById('saved-games-container');
        if (!savedGamesContainer) {
            savedGamesContainer = document.createElement('div');
            savedGamesContainer.id = 'saved-games-container';
            document.body.appendChild(savedGamesContainer); // Append to body or a specific section of your page
        }

        // Clear any existing content in the container
        savedGamesContainer.innerHTML = '';

        // Create and append elements for each saved game
        savedGames.forEach(function (game) {
            // Create a div for each saved game
            let gameElement = document.createElement('div');
            gameElement.className = 'saved-game';
            gameElement.innerHTML = 'Game saved on: ' + game.save_date;

            // Create a button to load each game
            let loadButton = document.createElement('button');
            loadButton.innerText = 'Load Game';
            loadButton.onclick = function () {
                loadGameState(game.user_id); // loadGameState function should be implemented to handle loading
            };

            // Append the load button to the game element
            gameElement.appendChild(loadButton);

            // Append the game element to the container
            savedGamesContainer.appendChild(gameElement);
        });
    }


    function loadGameState(userID) {
        console.log("In loadGameState()");

        $.ajax({
            url: "/api/GameStateAPI/loadBoard/" + userID, // Adjust the URL as per your routing
            type: "GET",
            success: function (gameState) {
                // Update the game board with the loaded state
                // This might involve parsing the gameState and updating each cell
            },
            error: function (error) {
                console.error("Error loading the game state: " + error);
            }
        });
    }

    function deleteGameState(userID) {
        console.log("In deleteGameState()");

        $.ajax({
            url: "/api/GameStateAPI/deleteBoard/" + userID, // Adjust the URL as per your routing
            type: "DELETE",
            success: function (result) {
                // Handle the UI changes after deletion (like removing the game from the list)
            },
            error: function (error) {
                console.error("Error deleting the game state: " + error);
            }
        });
    }



}); 
