using Quoter.App.Helpers.Extensions;
using Quoter.Framework.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Quoter.App.Services.FormAnimation
{
	public class FormAnimationsService : IFormAnimationService
	{
		private System.Timers.Timer _timerCloseDelay;

		public async Task AnimateAsync(Form form, EnumAnimation enumAnimation)
		{
			switch (enumAnimation)
			{
				case EnumAnimation.FadeInFromBottomRight:
					await FadeInFromBottomRight(form);
					break;
				case EnumAnimation.FadeOut:
					await FadeOut(form);
					break;
				case EnumAnimation.FadeIn:
					await FadeIn(form);
					break;
			}
		}

		//public async Task CloseDelayedAsync(Form form, int delay, EnumAnimation enumAnimation = EnumAnimation.FadeOut)
		//{
		//	Debug.WriteLine($"Start {nameof(CloseDelayedAsync)} Thread: {Thread.CurrentThread.ManagedThreadId}");
		//	if (_timerCloseDelay is not null && _timerCloseDelay.Enabled)
		//	{
		//		_timerCloseDelay.Stop();
		//		_timerCloseDelay.Dispose();
		//	}
		//	_timerCloseDelay = new(delay);
		//	_timerCloseDelay.Elapsed += async (sender, e) => await ElapsedTimerEventCloseDelayed(form, enumAnimation);
		//	_timerCloseDelay.Start();
		//}

		//private async Task ElapsedTimerEventCloseDelayed(Form form, EnumAnimation enumAnimation)
		//{
		//	Debug.WriteLine($"Start {nameof(ElapsedTimerEventCloseDelayed)} Thread: {Thread.CurrentThread.ManagedThreadId}");
		//	if (_timerCloseDelay is not null && _timerCloseDelay.Enabled)
		//	{
		//		_timerCloseDelay.Stop();
		//		_timerCloseDelay.Dispose();
		//	}
		//	await AnimateAsync(form, enumAnimation);
		//	if (!form.IsDisposed)
		//	{
		//		form.InvokeIfRequired(() =>
		//		{
		//			Debug.WriteLine($"Start {nameof(ElapsedTimerEventCloseDelayed)}.CloseForm Thread: {Thread.CurrentThread.ManagedThreadId}");
		//			form.Close();
		//		});
		//	}
		//}

		private async Task FadeInFromBottomRight(Form form)
		{
			Debug.WriteLine($"Start {nameof(FadeInFromBottomRight)} Thread: {Thread.CurrentThread.ManagedThreadId}");
			try
			{
				// Final expected location on scrren on the bottom right
				int locationX = Screen.PrimaryScreen.Bounds.Width - form.Width - 40;
				int locationY = Screen.PrimaryScreen.Bounds.Height - form.Height - 50;

				// Start from off screen to the right
				int currentPositionX = locationX + form.Width;
				form.InvokeIfRequired(() =>
				{
					Debug.WriteLine($"Start {nameof(FadeInFromBottomRight)} Set init opacity Thread: {Thread.CurrentThread.ManagedThreadId}");
					form.Opacity = 0.0;
					form.Location = new Point(currentPositionX, locationY);
				});

				// Move form frame by frame and increase opacity
				int frame = 20;
				int decrementPositionX = Math.Abs(currentPositionX - locationX) / frame;
				double incrementOpacity = (double)1 / frame;
				while (frame > 0)
				{
					await Task.Delay(5);
					currentPositionX -= decrementPositionX;
					form.InvokeIfRequired(() =>
					{
						form.Opacity += incrementOpacity;
						form.Location = new Point(currentPositionX, locationY);
						form.Update();
					});
					frame--;
					Debug.WriteLine($"	while {nameof(FadeInFromBottomRight)} Thread: {Thread.CurrentThread.ManagedThreadId}");
				}
			}
			catch(Exception ex)
			{
				Debug.WriteLine(ex.ToString());
			}
			
		}

		private async Task FadeOut(Form form)
		{
			// Decrease opacity frame by frame
			int frame = 20;
			double decrementOpacity = (double)1 / frame;
			while (frame > 0)
			{
				await Task.Delay(5);
				form.InvokeIfRequired(() =>
				{
					form.Opacity -= decrementOpacity;
					form.Update();
				});
				frame--;
			}
		}

		private async Task FadeIn(Form form)
		{
			// Decrease opacity frame by frame
			int frame = 20;
			form.InvokeIfRequired(() =>
			{
				form.Opacity = 0.0;
			});
			double incrementOpacity = (double)1 / frame;
			while (frame > 0)
			{
				await Task.Delay(5);
				form.InvokeIfRequired(() =>
				{
					form.Opacity += incrementOpacity;
					form.Update();
				});
				frame--;
			}
		}

	}
}
