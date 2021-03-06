﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;
using RoomSearch.CollectorEngine.Configuration;
using Quartz;
using Quartz.Impl;

namespace RoomSearch.CollectorEngine
{
    partial class CollectorService : ServiceBase
    {
        private JobDetail jobDetail;
        private CronTrigger cronTrigger;
        private ISchedulerFactory schedulerFactory;
        private IScheduler scheduler;

        public CollectorService()
        {
            InitializeComponent();
            InitializeScheduler();
        }

        private void InitializeScheduler()
        {
            try
            {
                if (scheduler != null) { return; }

                Logger.Log.Debug("Initializing Collector scheduler...");
                schedulerFactory = new StdSchedulerFactory();
                scheduler = schedulerFactory.GetScheduler();

                // KenhNhaTroJob
                jobDetail = new JobDetail("KenhNhaTroJob", null, typeof(KenhNhaTroJob));
                string cronEx = CollectorConfiguration.KenhNhaTro.KenhNhaTroCronTriggerExpression;
                cronTrigger = new CronTrigger(
                    "KenhNhaTroTrigger",
                    null,
                    cronEx
                    );
                cronTrigger.StartTimeUtc = DateTime.UtcNow;
                scheduler.ScheduleJob(jobDetail, cronTrigger);

                // BatDongSanJob
                jobDetail = new JobDetail("BatDongSanJob", null, typeof(BatDongSanJob));
                cronEx = CollectorConfiguration.BatDongSan.BatDongSanCronTriggerExpression;
                cronTrigger = new CronTrigger(
                    "BatDongSanTrigger",
                    null,
                    cronEx
                    );
                cronTrigger.StartTimeUtc = DateTime.UtcNow;
                scheduler.ScheduleJob(jobDetail, cronTrigger);

                // RongBayJob
                jobDetail = new JobDetail("RongBayJob", null, typeof(RongBayJob));
                cronEx = CollectorConfiguration.RongBay.RongBayCronTriggerExpression;
                cronTrigger = new CronTrigger(
                    "RongBayTrigger",
                    null,
                    cronEx
                    );
                cronTrigger.StartTimeUtc = DateTime.UtcNow;
                scheduler.ScheduleJob(jobDetail, cronTrigger);

                Logger.Log.Debug("Initialize Collector scheduler completely.");
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
            }
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                Logger.Log.Debug("Starting Collector scheduler...");
                scheduler.Start();
                Logger.Log.Debug("Start Collector scheduler completely.");
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
            }
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
    }
}
