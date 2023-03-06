﻿using System;
using System.Collections.Generic;
using DefaultNamespace.Attack;
using UnityEngine;

namespace Character
{
    [Serializable]
    public class CharacterAttributes
    {
        [SerializeField] private string name;
        [SerializeField] private Sprite sprite;
        [SerializeField] private int baseHealth;
        [SerializeField] private int baseAttackPower;
        [SerializeField] private List<AttackType> skillSet;

        public string Name => name;
        public Sprite Sprite => sprite;
        public int BaseHealth => baseHealth;
        public int BaseAttackPower => baseAttackPower;
        public List<AttackType> SkillSet => skillSet;

    }
    
}