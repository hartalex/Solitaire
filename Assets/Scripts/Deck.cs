using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Solitaire
{
    public class Deck : Pile 
    {
		public bool initialized = false;
		public GameObject cardprefab = null;
		public Text heightText;
		public HeightDirection direction;
		public Material[] materials;
		public Deck() : base() {}

		public void Init() {
			if (!initialized) {
				this.AddCardToTop(CreateCard(Suit.Diamond, Rank.Ace, materials[0]), true);
				this.AddCardToTop(CreateCard(Suit.Diamond, Rank.Two, materials[1]), true);
				this.AddCardToTop(CreateCard(Suit.Diamond, Rank.Three, materials[2]), true);
				this.AddCardToTop(CreateCard(Suit.Diamond, Rank.Four, materials[3]), true);
				this.AddCardToTop(CreateCard(Suit.Diamond, Rank.Five, materials[4]), true);
				this.AddCardToTop(CreateCard(Suit.Diamond, Rank.Six, materials[5]), true);
				this.AddCardToTop(CreateCard(Suit.Diamond, Rank.Seven, materials[6]), true);
				this.AddCardToTop(CreateCard(Suit.Diamond, Rank.Eight, materials[7]), true);
				this.AddCardToTop(CreateCard(Suit.Diamond, Rank.Nine, materials[8]), true);
				this.AddCardToTop(CreateCard(Suit.Diamond, Rank.Ten, materials[9]), true);
				this.AddCardToTop(CreateCard(Suit.Diamond, Rank.Jack, materials[10]), true);
				this.AddCardToTop(CreateCard(Suit.Diamond, Rank.Queen, materials[11]), true);
				this.AddCardToTop(CreateCard(Suit.Diamond, Rank.King, materials[12]), true);

				this.AddCardToTop(CreateCard(Suit.Club, Rank.Ace, materials[13]), true);
				this.AddCardToTop(CreateCard(Suit.Club, Rank.Two, materials[14]), true);
				this.AddCardToTop(CreateCard(Suit.Club, Rank.Three, materials[15]), true);
				this.AddCardToTop(CreateCard(Suit.Club, Rank.Four, materials[16]), true);
				this.AddCardToTop(CreateCard(Suit.Club, Rank.Five, materials[17]), true);
				this.AddCardToTop(CreateCard(Suit.Club, Rank.Six, materials[18]), true);
				this.AddCardToTop(CreateCard(Suit.Club, Rank.Seven, materials[19]), true);
				this.AddCardToTop(CreateCard(Suit.Club, Rank.Eight, materials[20]), true);
				this.AddCardToTop(CreateCard(Suit.Club, Rank.Nine, materials[21]), true);
				this.AddCardToTop(CreateCard(Suit.Club, Rank.Ten, materials[22]), true);
				this.AddCardToTop(CreateCard(Suit.Club, Rank.Jack, materials[23]), true);
				this.AddCardToTop(CreateCard(Suit.Club, Rank.Queen, materials[24]), true);
				this.AddCardToTop(CreateCard(Suit.Club, Rank.King, materials[25]), true);

				this.AddCardToTop(CreateCard(Suit.Heart, Rank.Ace, materials[26]), true);
				this.AddCardToTop(CreateCard(Suit.Heart, Rank.Two, materials[27]), true);
				this.AddCardToTop(CreateCard(Suit.Heart, Rank.Three, materials[28]), true);
				this.AddCardToTop(CreateCard(Suit.Heart, Rank.Four, materials[29]), true);
				this.AddCardToTop(CreateCard(Suit.Heart, Rank.Five, materials[30]), true);
				this.AddCardToTop(CreateCard(Suit.Heart, Rank.Six, materials[31]), true);
				this.AddCardToTop(CreateCard(Suit.Heart, Rank.Seven, materials[32]), true);
				this.AddCardToTop(CreateCard(Suit.Heart, Rank.Eight, materials[33]), true);
				this.AddCardToTop(CreateCard(Suit.Heart, Rank.Nine, materials[34]), true);
				this.AddCardToTop(CreateCard(Suit.Heart, Rank.Ten, materials[35]), true);
				this.AddCardToTop(CreateCard(Suit.Heart, Rank.Jack, materials[36]), true);
				this.AddCardToTop(CreateCard(Suit.Heart, Rank.Queen, materials[37]), true);
				this.AddCardToTop(CreateCard(Suit.Heart, Rank.King, materials[38]), true);

				this.AddCardToTop(CreateCard(Suit.Spade, Rank.Ace, materials[39]), true);
				this.AddCardToTop(CreateCard(Suit.Spade, Rank.Two, materials[40]), true);
				this.AddCardToTop(CreateCard(Suit.Spade, Rank.Three, materials[41]), true);
				this.AddCardToTop(CreateCard(Suit.Spade, Rank.Four, materials[42]), true);
				this.AddCardToTop(CreateCard(Suit.Spade, Rank.Five, materials[43]), true);
				this.AddCardToTop(CreateCard(Suit.Spade, Rank.Six, materials[44]), true);
				this.AddCardToTop(CreateCard(Suit.Spade, Rank.Seven, materials[45]), true);
				this.AddCardToTop(CreateCard(Suit.Spade, Rank.Eight, materials[46]), true);
				this.AddCardToTop(CreateCard(Suit.Spade, Rank.Nine, materials[47]), true);
				this.AddCardToTop(CreateCard(Suit.Spade, Rank.Ten, materials[48]), true);
				this.AddCardToTop(CreateCard(Suit.Spade, Rank.Jack, materials[49]), true);
				this.AddCardToTop(CreateCard(Suit.Spade, Rank.Queen, materials[50]), true);
				this.AddCardToTop(CreateCard(Suit.Spade, Rank.King, materials[51]), true);

				// Set MaxHeightUI Text Element
				if (heightText != null) {
					StaticMaxHeight.uiText = heightText;
					StaticMaxHeight.direction = direction;
				}
				initialized = true;
			}
		}

		public void Start() {
			Init ();
        }

		public void Update() {
			Init ();
		}

		protected Card CreateCard(Suit suit, Rank rank, Material material)
        {
            Card retval = null;
            GameObject gameObjectCard = GameObject.Instantiate(cardprefab);

            retval = gameObjectCard.GetComponent<Card>();
            retval.suit = suit;
            retval.rank = rank;
            retval.facingUp = false;
			gameObjectCard.layer = 9;

            MeshRenderer mr = gameObjectCard.GetComponentInChildren<MeshRenderer>();
            if (mr != null)
            {
                Material[] newMaterials = { mr.materials[0], mr.materials[1], material };
                mr.materials = newMaterials;
            }
            else
            {
                SkinnedMeshRenderer smr = gameObjectCard.GetComponentInChildren<SkinnedMeshRenderer>();
                if (smr != null)
                {
                    Material[] newMaterials = { smr.materials[0], smr.materials[1], material };
                    smr.materials = newMaterials;
                }
            }
            gameObjectCard.name = retval.ToShortName();
            gameObjectCard.transform.localPosition = new Vector3();

			return retval;
        }

    }
}
