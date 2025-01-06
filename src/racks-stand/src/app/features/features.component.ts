import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-features',
  imports: [CommonModule],
  templateUrl: './features.component.html',
  styleUrl: './features.component.scss'
})
export class FeaturesComponent {
  features = [
    {
      icon: 'assets/icons/real-time.svg',
      title: 'Real-Time Inventory Tracking',
      description: 'Monitor your inventory levels in real-time with accurate data.',
    },
    {
      icon: 'assets/icons/custom-workflows.svg',
      title: 'Customizable Workflows',
      description: 'Adapt the platform to your business processes effortlessly.',
    },
    {
      icon: 'assets/icons/reporting.svg',
      title: 'Comprehensive Reporting',
      description: 'Generate insightful reports to make data-driven decisions.',
    },
    {
      icon: 'assets/icons/integrations.svg',
      title: 'Seamless Integrations',
      description: 'Easily integrate with popular tools and platforms.',
    },
  ];
}
