using System;
using System.Collections.Generic;

using CocosSharp;

namespace Match3.Entities
{
    public class Chain
    {
        public ChainType chainType;
        public int chainCount;
        public int[] chainLengths = new int[2];
        public string chainDirection;

        public enum ChainType
        {
            Horizontal, Vertical,
        };

        public void UpdateChainLengths(string direction, int amount)
        {
            if(direction == "Horizontal")
            {
                chainLengths[0] += amount;
            }
            else if(direction == "Vertical"){
                chainLengths[1] += amount;
            }
            else
            {
                throw new System.Exception("Direction sent to update chain length must be Horizontal or Vertical");
            }
        }

        //public Chain()
        //{
        //    materials = new List<Material>();
        //}

        //public void addMaterial(Material material)
        //{
        //    materials.Add(material);
        //}
    }
}