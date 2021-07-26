﻿using OutSystems.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelAPITest
{
    interface ElementToggle
    {
        void GetDiffElements(IESpace old, IESpace newe, String newOrAltered);

        void GetAllElements(IESpace newe);

        void InsertIf(IESpace espace, List<IKey> keys, String feature, String prefix);

        void GetAllElementsFromList(IESpace newe, List<string> elements, String feature, String prefix);
    }
}
