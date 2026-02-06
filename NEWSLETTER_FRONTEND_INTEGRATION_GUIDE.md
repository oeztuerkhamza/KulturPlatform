# ?? Newsletter & Azure Email Service - Frontend Integration Guide

## ?? Genel Bak??

Backend'de Azure Communication Services Email entegrasyonu ve Newsletter sistemi tamamland?. Bu doküman frontend developer'lar? için entegrasyon rehberidir.

---

## ?? H?zl? Ba?lang?ç

### Backend Özellikleri
? **Azure Communication Services Email** deste?i (ilk 100 e-posta/ay ücretsiz)
? **SMTP fallback** mekanizmas?
? **Double Opt-In** newsletter subscription
? **Newsletter Campaign** yönetimi
? **Admin panel** için endpoint'ler

---

## ?? API Endpoints

### 1. Newsletter Subscription (Public - Kullan?c?lar ?çin)

#### **Subscribe to Newsletter**
```typescript
POST /api/newsletter/subscribe

// Request Body
{
  "email": "user@example.com",
  "fullName": "John Doe" // Opsiyonel
}

// Response (200 OK)
{
  "success": true,
  "message": "Verification email sent. Please check your inbox."
}
```

**Frontend Implementation Örne?i:**
```typescript
// components/NewsletterSubscriptionForm.tsx
import { useState } from 'react';

export default function NewsletterSubscriptionForm() {
  const [email, setEmail] = useState('');
  const [fullName, setFullName] = useState('');
  const [loading, setLoading] = useState(false);
  const [message, setMessage] = useState('');

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);

    try {
      const response = await fetch('/api/newsletter/subscribe', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email, fullName })
      });

      const data = await response.json();
      setMessage(data.message);
      
      if (data.success) {
        setEmail('');
        setFullName('');
      }
    } catch (error) {
      setMessage('Ein Fehler ist aufgetreten. Bitte versuchen Sie es später erneut.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <form onSubmit={handleSubmit} className="newsletter-form">
      <input
        type="email"
        value={email}
        onChange={(e) => setEmail(e.target.value)}
        placeholder="Ihre E-Mail-Adresse"
        required
      />
      <input
        type="text"
        value={fullName}
        onChange={(e) => setFullName(e.target.value)}
        placeholder="Ihr Name (optional)"
      />
      <button type="submit" disabled={loading}>
        {loading ? 'Wird geladen...' : 'Abonnieren'}
      </button>
      {message && <p className="message">{message}</p>}
    </form>
  );
}
```

---

#### **Verify Subscription (Double Opt-In)**
```typescript
GET /api/newsletter/verify?token={TOKEN}

// Response (200 OK)
{
  "success": true,
  "message": "Newsletter subscription verified successfully!"
}
```

**Frontend Route:**
```typescript
// pages/newsletter/verify.tsx
import { useEffect, useState } from 'react';
import { useRouter } from 'next/router';

export default function VerifyNewsletterPage() {
  const router = useRouter();
  const { token } = router.query;
  const [status, setStatus] = useState<'loading' | 'success' | 'error'>('loading');
  const [message, setMessage] = useState('');

  useEffect(() => {
    if (token) {
      verifySubscription(token as string);
    }
  }, [token]);

  const verifySubscription = async (token: string) => {
    try {
      const response = await fetch(`/api/newsletter/verify?token=${token}`);
      const data = await response.json();
      
      setStatus(data.success ? 'success' : 'error');
      setMessage(data.message);
    } catch (error) {
      setStatus('error');
      setMessage('Verifizierung fehlgeschlagen.');
    }
  };

  return (
    <div className="verify-page">
      {status === 'loading' && <p>Wird verifiziert...</p>}
      {status === 'success' && (
        <div className="success">
          <h1>? Erfolgreich!</h1>
          <p>{message}</p>
        </div>
      )}
      {status === 'error' && (
        <div className="error">
          <h1>? Fehler</h1>
          <p>{message}</p>
        </div>
      )}
    </div>
  );
}
```

---

#### **Unsubscribe**
```typescript
GET /api/newsletter/unsubscribe?token={TOKEN}

// Response (200 OK)
{
  "success": true,
  "message": "Successfully unsubscribed from newsletter."
}
```

**Frontend Route:**
```typescript
// pages/newsletter/unsubscribe.tsx
// Similar to verify page, yukar?daki örne?i adapte edin
```

---

### 2. Admin Panel Endpoints (Require Auth)

#### **Get Subscribers**
```typescript
GET /api/newsletter/subscribers
Authorization: Bearer {JWT_TOKEN}

// Response (200 OK)
[
  {
    "id": "guid",
    "email": "user@example.com",
    "fullName": "John Doe",
    "isActive": true,
    "isVerified": true,
    "subscribedAt": "2025-01-15T10:30:00Z",
    "verifiedAt": "2025-01-15T10:35:00Z",
    "source": "Website"
  }
]
```

---

#### **Get Subscriber Statistics**
```typescript
GET /api/newsletter/subscribers/stats
Authorization: Bearer {JWT_TOKEN}

// Response (200 OK)
{
  "totalSubscribers": 150,
  "activeSubscribers": 120,
  "verifiedSubscribers": 120,
  "unsubscribedCount": 30,
  "totalCampaignsSent": 5,
  "totalEmailsSent": 600
}
```

**Frontend Dashboard Component:**
```typescript
// components/admin/NewsletterStats.tsx
export default function NewsletterStats() {
  const [stats, setStats] = useState(null);

  useEffect(() => {
    fetchStats();
  }, []);

  const fetchStats = async () => {
    const response = await fetch('/api/newsletter/subscribers/stats', {
      headers: { 'Authorization': `Bearer ${getJwtToken()}` }
    });
    const data = await response.json();
    setStats(data);
  };

  if (!stats) return <div>Loading...</div>;

  return (
    <div className="stats-grid">
      <StatCard title="Total Subscribers" value={stats.totalSubscribers} />
      <StatCard title="Active Subscribers" value={stats.activeSubscribers} />
      <StatCard title="Campaigns Sent" value={stats.totalCampaignsSent} />
      <StatCard title="Total Emails Sent" value={stats.totalEmailsSent} />
    </div>
  );
}
```

---

#### **Create Newsletter Campaign**
```typescript
POST /api/newsletter/campaigns
Authorization: Bearer {JWT_TOKEN}

// Request Body
{
  "subject": "January Newsletter 2025",
  "contentTr": "<p>Merhaba {{name}},</p><p>Yeni haberlerimiz...</p>",
  "contentDe": "<p>Hallo {{name}},</p><p>Unsere neuesten Nachrichten...</p>",
  "headerImageUrl": "https://example.com/header.jpg", // Opsiyonel
  "scheduledAt": "2025-01-20T10:00:00Z" // Opsiyonel - null ise draft olarak kal?r
}

// Response (201 Created)
{
  "id": "campaign-guid"
}
```

**Desteklenen Template Variables:**
- `{{name}}` - Subscriber's name (veya "Werte Leserin/Leser")
- `{{email}}` - Subscriber's email

---

#### **Get All Campaigns**
```typescript
GET /api/newsletter/campaigns
Authorization: Bearer {JWT_TOKEN}

// Response (200 OK)
[
  {
    "id": "guid",
    "subject": "January Newsletter",
    "contentTr": "...",
    "contentDe": "...",
    "headerImageUrl": "...",
    "status": "Sent", // Draft | Scheduled | Sending | Sent | Failed
    "createdAt": "2025-01-10T10:00:00Z",
    "scheduledAt": null,
    "sentAt": "2025-01-15T12:00:00Z",
    "totalRecipients": 120,
    "successfulSends": 118,
    "failedSends": 2
  }
]
```

---

#### **Send Campaign**
```typescript
POST /api/newsletter/campaigns/{id}/send
Authorization: Bearer {JWT_TOKEN}

// Response (200 OK)
{
  "success": true,
  "message": "Campaign sent successfully",
  "successful": 118,
  "failed": 2,
  "total": 120
}
```

**Frontend Campaign Send Component:**
```typescript
// components/admin/CampaignSendButton.tsx
export default function CampaignSendButton({ campaignId }: { campaignId: string }) {
  const [sending, setSending] = useState(false);

  const handleSend = async () => {
    if (!confirm('Bu kampanyay? tüm abonelere göndermek istedi?inizden emin misiniz?')) {
      return;
    }

    setSending(true);
    try {
      const response = await fetch(`/api/newsletter/campaigns/${campaignId}/send`, {
        method: 'POST',
        headers: { 'Authorization': `Bearer ${getJwtToken()}` }
      });
      
      const data = await response.json();
      alert(`? Gönderildi! Ba?ar?l?: ${data.successful}, Ba?ar?s?z: ${data.failed}`);
    } catch (error) {
      alert('? Gönderim ba?ar?s?z oldu.');
    } finally {
      setSending(false);
    }
  };

  return (
    <button onClick={handleSend} disabled={sending}>
      {sending ? 'Gönderiliyor...' : 'Kampanyay? Gönder'}
    </button>
  );
}
```

---

#### **Send Test Email**
```typescript
POST /api/newsletter/campaigns/{id}/test
Authorization: Bearer {JWT_TOKEN}

// Request Body
{
  "campaignId": "campaign-guid",
  "testEmail": "admin@example.com"
}

// Response (200 OK)
{
  "success": true,
  "message": "Test email sent successfully"
}
```

---

## ?? UI/UX Önerileri

### 1. Homepage - Newsletter Subscription Widget
```typescript
// Önerilen yerle?im: Footer'da veya dedicated section
<section className="newsletter-section">
  <h2>Bleiben Sie auf dem Laufenden!</h2>
  <p>Abonnieren Sie unseren Newsletter für die neuesten Updates</p>
  <NewsletterSubscriptionForm />
</section>
```

### 2. Verification Page Design
- **Success State:** Ye?il checkmark, "Vielen Dank!" mesaj?
- **Error State:** K?rm?z? warning, tekrar subscribe linki
- **Loading State:** Spinner animasyonu

### 3. Admin Panel - Campaign Manager
**Önerilen Özellikler:**
- ? Campaign list (table/grid view)
- ? Draft/Scheduled/Sent status badges
- ? Rich text editor (TinyMCE, Quill, veya Draft.js)
- ? Preview mode
- ? Send test email button
- ? Statistics dashboard

---

## ?? Azure Configuration (Backend Admin ?çin)

### Ad?m 1: Azure Communication Services Olu?turma
```bash
# Azure CLI ile
az communication create \
  --name kulturplatform-email \
  --resource-group kulturplatform-rg \
  --location "westeurope" \
  --data-location "Europe"
```

### Ad?m 2: Connection String Alma
```bash
az communication list-key \
  --name kulturplatform-email \
  --resource-group kulturplatform-rg
```

### Ad?m 3: appsettings.json Güncelleme
```json
{
  "EmailSettings": {
    "Provider": "AzureCommunicationServices", // SMTP'den ACS'ye geçi?
    "AzureConnectionString": "endpoint=https://...;accesskey=...",
    "AzureSenderEmail": "DoNotReply@kulturplattformfreiburg.org",
    "MaxEmailsPerHour": "1000" // Azure ile art?r?labilir
  }
}
```

### Ad?m 4: Domain Verification
1. Azure Portal > Email Communication Services > Domains
2. Add Custom Domain: `kulturplattformfreiburg.org`
3. DNS records ekleyin:
```
TXT @ "v=spf1 include:_spf.azurecomm.net ~all"
CNAME selector1._domainkey IN selector1-kulturplattformfreiburg-org._domainkey.azurecomm.net
```

---

## ?? Database Migration

Backend'de migration otomatik olarak uygulan?r (Program.cs). Eklenecek tablolar:

```sql
-- NewsletterSubscribers
CREATE TABLE NewsletterSubscribers (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Email NVARCHAR(255) UNIQUE NOT NULL,
    FullName NVARCHAR(200),
    IsActive BIT DEFAULT 1,
    IsVerified BIT DEFAULT 0,
    SubscribedAt DATETIME2 NOT NULL,
    VerifiedAt DATETIME2,
    UnsubscribedAt DATETIME2,
    UnsubscribeToken NVARCHAR(50) UNIQUE NOT NULL,
    VerificationToken NVARCHAR(50) UNIQUE,
    Source NVARCHAR(50) NOT NULL
);

-- NewsletterCampaigns
CREATE TABLE NewsletterCampaigns (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Subject NVARCHAR(500) NOT NULL,
    ContentTr NVARCHAR(MAX) NOT NULL,
    ContentDe NVARCHAR(MAX) NOT NULL,
    HeaderImageUrl NVARCHAR(500),
    Status NVARCHAR(50) NOT NULL,
    CreatedAt DATETIME2 NOT NULL,
    ScheduledAt DATETIME2,
    SentAt DATETIME2,
    TotalRecipients INT DEFAULT 0,
    SuccessfulSends INT DEFAULT 0,
    FailedSends INT DEFAULT 0,
    CreatedBy UNIQUEIDENTIFIER NOT NULL
);
```

---

## ?? Testing Scenarios

### Frontend Testing Checklist
- [ ] Newsletter subscription form validation
- [ ] Email format validation
- [ ] Success/error message display
- [ ] Loading states
- [ ] Verification page (success/error states)
- [ ] Unsubscribe page
- [ ] Admin campaign list view
- [ ] Campaign creation with rich text editor
- [ ] Send test email functionality
- [ ] Campaign statistics display

### Backend Testing (Postman/Insomnia)
1. **Subscribe:** POST `/api/newsletter/subscribe`
2. **Verify:** GET `/api/newsletter/verify?token=...`
3. **Get Stats:** GET `/api/newsletter/subscribers/stats`
4. **Create Campaign:** POST `/api/newsletter/campaigns`
5. **Send Test:** POST `/api/newsletter/campaigns/{id}/test`
6. **Send Campaign:** POST `/api/newsletter/campaigns/{id}/send`

---

## ?? Security Best Practices

### Frontend
- ? CSRF protection (cookies with SameSite attribute)
- ? Input sanitization (email validation)
- ? Rate limiting awareness (inform users)

### Backend (Zaten Implement Edildi)
- ? Rate limiting (100 emails/hour configurable)
- ? HTML sanitization (XSS protection)
- ? Anti-spam headers
- ? Double opt-in (GDPR compliance)
- ? JWT authentication for admin endpoints

---

## ?? Email Template Customization

Newsletter email'leri responsive HTML template kullan?r. Özelle?tirme için:

1. **Header Image:** Campaign olu?tururken `headerImageUrl` parametresi
2. **Content:** HTML tags desteklenir (sanitized)
3. **Variables:** `{{name}}`, `{{email}}` kullan?labilir
4. **Unsubscribe Link:** Otomatik eklenir (her email'in footer'?nda)

---

## ?? Deployment Checklist

### Production'a Geçerken
- [ ] Azure Connection String environment variable'a ta??
- [ ] `Provider: "AzureCommunicationServices"` yap
- [ ] Domain verification tamamla
- [ ] SPF/DKIM records ekle
- [ ] Base URL'i production URL ile de?i?tir
- [ ] SMTP fallback test et
- [ ] Monitoring/logging kurulumunu do?rula

---

## ?? Support & Questions

Herhangi bir soru veya problem için:
- Backend API documentation: `/swagger` (development)
- Email Service logs: Azure Application Insights
- Rate limiting issues: `EmailSettings:MaxEmailsPerHour` art?r

---

## ?? Quick Start Summary

**Frontend Developer için 3 Ad?m:**

1. **Newsletter Widget Ekle (Public)**
   ```tsx
   import NewsletterForm from '@/components/NewsletterForm'
   
   <NewsletterForm />
   ```

2. **Verification Pages Olu?tur**
   - `/newsletter/verify?token=...`
   - `/newsletter/unsubscribe?token=...`

3. **Admin Panel Entegrasyonu (Optional)**
   - Campaign list view
   - Campaign creation form
   - Send campaign button
   - Statistics dashboard

**Backend Admin için 2 Ad?m:**

1. **Azure Setup**
   - Communication Services olu?tur
   - Connection String al
   - appsettings.json'a ekle

2. **Provider De?i?tir**
   ```json
   "Provider": "AzureCommunicationServices"
   ```

---

**? Backend Haz?r! Frontend integration ba?layabilir.**

**?? Maliyet:** ?lk 100 e-posta/ay **ücretsiz**! ??
