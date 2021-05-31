﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OutSystems.Model;
using OutSystems.Model.Data;
using ServiceStudio.Plugin.REST;

namespace ModelAPITest.ToggleElements
{
    class ToggleEntities
    {
        private const string TogglesEntity = "FeatureToggles";
        public IStaticEntity GetTogglesEntity(IESpace espace)
        {
            var entity = (IStaticEntity)espace.Entities.SingleOrDefault(s => s.Name == TogglesEntity);
            if(entity == default) 
            {
                return CreateEntity(espace); 
            }
            else 
            {
                return entity; 
            }
        }
        public IStaticEntity CreateEntity(IESpace espace)
        {
            var entity = espace.CreateStaticEntity(TogglesEntity);
            var keyatt = entity.CreateAttribute("Key");
            keyatt.DataType = espace.TextType;
            keyatt.Length = 100;
            keyatt.IsMandatory = true;

            var labelatt = entity.CreateAttribute("Label");
            keyatt.DataType = espace.TextType;
            keyatt.Length = 100;
            keyatt.IsMandatory = true;

            entity.Public = false;
            entity.IdentifierAttribute = keyatt;
            entity.LabelAttribute = labelatt;
            return entity;
        }

        public IRecord CreateRecord(IStaticEntity entity, String key, String label)
        {
            IEntityAttribute GetAttribute(string name) => entity.Attributes.Single(a => a.Name == name);
            var rec = entity.Records;
            var exists = rec.SingleOrDefault(s => s.ToString().Contains(key));

            
            if (exists == default)
            {
                var record = entity.CreateRecord();
                record.Identifier = key;
                var keyatt = GetAttribute("Key");
                record.SetAttributeValue(keyatt, $"\"{key}\"");
                var labelatt = GetAttribute("Label");
                record.SetAttributeValue(labelatt, $"\"{label}\"");
                return record;
            }

            return null;
        }


        public void CreateToggle(IESpace espace, IStaticEntity entity, String key, String label)
        {
            var restAPI = espace.Integrations.Single(i => i.Name == "FeatureToggleAPI");
            restAPI.GetAllDescendantsOfType<IRestAction>().Single(a => a.Name == "EV_FeatureToggle_CreateOrUpdate");

            
        }

    }
}
