using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace fairwell
{
    public class ModuleFairwell : PartModule
    {
        [KSPField(guiActive = true, guiActiveEditor = true, isPersistant = true, guiName = "Fairwell active")]
        private bool isOn = false;

        [KSPEvent(active = true, guiActive = true, guiActiveEditor = true, externalToEVAOnly = false, guiActiveUnfocused = false, guiName = "Toggle Fairwell")]
        private void toggleOn()
        {
            if (isOn)
            {
                isOn = false;
            }
            else
            {
                isOn = true;
            }
        }

        [UI_FloatRange(minValue = 100, maxValue = 90000, stepIncrement = 100)]
        [KSPField(isPersistant = true, guiActiveEditor = true, guiActive = true, guiName = "Deployment Altitude", guiFormat = "F0")]
        public float deploymentAltitude = 60000;
        
        public void FixedUpdate()
        {
            if (!HighLogic.LoadedSceneIsFlight) return;
            if (!isOn) return;
            if (isOn)
            {
                if (this.part.vessel.altitude >= deploymentAltitude)
                {
                    if (this.part.Modules.Contains("ModuleProceduralFairing"))
                    {
                        var pm = this.part.Modules.OfType<ModuleProceduralFairing>().Single();
                        pm = this.part.FindModulesImplementing<ModuleProceduralFairing>().First();
                        pm.DeployFairing();
                        isOn = false;
                    }
                }
            }
        }
    }
}
