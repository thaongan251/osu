// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Game.Beatmaps.ControlPoints;
using osu.Game.Graphics.UserInterfaceV2;

namespace osu.Game.Screens.Edit.Timing
{
    internal class EffectSection : Section<EffectControlPoint>
    {
        private LabelledSwitchButton kiai;
        private LabelledSwitchButton omitBarLine;

        [BackgroundDependencyLoader]
        private void load()
        {
            Flow.AddRange(new[]
            {
                kiai = new LabelledSwitchButton { Label = "Kiai Time" },
                omitBarLine = new LabelledSwitchButton { Label = "Skip Bar Line" },
            });
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            ControlPoint.BindValueChanged(point =>
            {
                if (point.NewValue != null)
                {
                    kiai.Current = point.NewValue.KiaiModeBindable;
                    omitBarLine.Current = point.NewValue.OmitFirstBarLineBindable;
                }
            });
        }

        protected override EffectControlPoint CreatePoint()
        {
            var reference = Beatmap.Value.Beatmap.ControlPointInfo.EffectPointAt(SelectedGroup.Value.Time);

            return new EffectControlPoint
            {
                KiaiMode = reference.KiaiMode,
                OmitFirstBarLine = reference.OmitFirstBarLine
            };
        }
    }
}
