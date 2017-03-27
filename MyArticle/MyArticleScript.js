function ChangeImage(s, e) {

    Thumbnail_ASPxImage.SetImageUrl(s.cpResult);
    Thumbnail_ASPxHiddenField.Add("ImageUrl", s.cpResult)
}