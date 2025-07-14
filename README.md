# ♠️ Tri-Peaks Solitaire (Unity Game)

Tri-Peaks Solitaire is a classic card puzzle game where the goal is to clear all the cards from the pyramid by playing cards that are one higher or one lower in value than the current card in the waste pile.

---

## 🎮 How to Play

### 🧱 Objective
Clear all cards from the pyramid to win the game.

---

### 📦 Game Layout

- **Pyramid:** 28 cards arranged in 3 overlapping peaks
- **Deck Pile:** Face-down stack of draw cards (default: 24 cards)
- **Waste Pile:** The active card the player uses to make matches

---

### ▶️ Gameplay Rules

1. **Start** the game — one card is drawn from the deck and placed on the waste pile.
2. **Play a pyramid card** if it is **one rank higher or lower** than the current waste card.
   - Example: If the waste pile shows a 5, you can play a 4 or 6.
   - **Ace** is both high and low:
     - It can connect to 2 and King.
3. **Only face-up cards** (unblocked) can be played.
   - Cards are face-up when they are not covered by any other cards.
4. If no playable cards are available:
   - **Draw a new card** from the deck to the waste pile.
   - You can continue drawing until the deck is empty.

---

### 🔁 Bonus Feature: Buy a New Deck

- If the deck is exhausted and no valid moves remain, the game shows a **Game Over** screen.
- You can choose to **"Buy a New Deck"** — this adds a new set of random cards to the deck.
- The new deck may give you another chance to win, but **winning is not guaranteed**.

---

### ✅ Win Condition
- You **win** if all cards in the pyramid are cleared.

---

### ❌ Lose Condition
- You **lose** if:
  - The deck is empty **and**
  - No valid moves are left in the pyramid

---
